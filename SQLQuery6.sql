CREATE TABLE Admin (
    Admin_ID INT IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(50) NOT NULL,
    Role NVARCHAR(50),
    Shift_Type NVARCHAR(50),
    Salary INT,
	PRIMARY KEY (Admin_ID)
);

CREATE TABLE Member (
    Member_ID INT IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Phone NVARCHAR(20),
    Weight DECIMAL(5, 2),
    Height DECIMAL(5, 2),
    Age INT,
    Gender NVARCHAR(10),
    Join_Date DATE,
    Membership_Type NVARCHAR(50),
    Goal NVARCHAR(200),
    Last_Updated DATE,
	PRIMARY KEY (Member_ID)
);

CREATE TABLE Receptionist (
    Receptionist_ID INT IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(50) NOT NULL,
    Shift_Type NVARCHAR(50),
    Salary INT,
    Admin_ID INT,
    FOREIGN KEY (Admin_ID) REFERENCES Admin(Admin_ID),
	PRIMARY KEY (Receptionist_ID)
);

CREATE TABLE Trainer (
    Trainer_ID INT IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    Specialty NVARCHAR(100),
    Experience_Years INT,
    Salary INT,
    Admin_ID INT,
    FOREIGN KEY (Admin_ID) REFERENCES Admin(Admin_ID),
	PRIMARY KEY (Trainer_ID)
);

CREATE TABLE Class (
    Class_ID INT IDENTITY(1,1),
    Class_Name NVARCHAR(50) NOT NULL,
    Date DATE NOT NULL,
    Start_Time TIME NOT NULL,
    End_Time TIME NOT NULL,
    Capacity INT DEFAULT 20,
    Trainer_ID INT,
    Admin_ID INT,
    FOREIGN KEY (Trainer_ID) REFERENCES Trainer(Trainer_ID),
    FOREIGN KEY (Admin_ID) REFERENCES Admin(Admin_ID),
	PRIMARY KEY (Class_ID)
);

CREATE TABLE Class_Enrollment (
    Enrollment_ID INT IDENTITY(1,1),
    Member_ID INT NOT NULL,
	Trainer_ID INT NOT NULL,
    Class_ID INT NOT NULL,
    Enrollment_Date DATE DEFAULT GETDATE(),
    FOREIGN KEY (Member_ID) REFERENCES Member(Member_ID),
	FOREIGN KEY (Trainer_ID) REFERENCES Trainer(Trainer_ID),
    FOREIGN KEY (Class_ID) REFERENCES Class(Class_ID),
	PRIMARY KEY (Enrollment_ID)
);

CREATE TABLE Attendance (
    Attendance_ID INT IDENTITY(1,1),
    Member_ID INT NOT NULL,
    Receptionist_ID INT,
    Date DATE DEFAULT GETDATE(),
    Time_In TIME,
    Time_Out TIME,
    Status NVARCHAR(20),
    FOREIGN KEY (Member_ID) REFERENCES Member(Member_ID),
    FOREIGN KEY (Receptionist_ID) REFERENCES Receptionist(Receptionist_ID),
	PRIMARY KEY (Attendance_ID)
);

CREATE TABLE Workout_Plan (
    Plan_ID INT IDENTITY(1,1),
    Member_ID INT NOT NULL,
    Trainer_ID INT NOT NULL,
    Weekly_Schedule NVARCHAR(MAX),
    Focus_Area NVARCHAR(200),
    Last_Updated DATE,
    FOREIGN KEY (Member_ID) REFERENCES Member(Member_ID),
    FOREIGN KEY (Trainer_ID) REFERENCES Trainer(Trainer_ID),
	PRIMARY KEY (Plan_ID)
);

CREATE TABLE Diet_Plan (
    DietPlan_ID INT IDENTITY(1,1),
    Member_ID INT NOT NULL,
    Trainer_ID INT NOT  NULL,
    Daily_Calories INT,
    Water_Intake_Goal INT,
    Allergies NVARCHAR(200),
    Last_Updated DATE,
    FOREIGN KEY (Member_ID) REFERENCES Member(Member_ID),
    FOREIGN KEY (Trainer_ID) REFERENCES Trainer(Trainer_ID),
	PRIMARY KEY (DietPlan_ID)
);

CREATE TABLE Progress (
    Progress_ID INT IDENTITY(1,1),
    Member_ID INT NOT NULL,
    Trainer_ID INT NOT NULL,
    Date_Recorded DATE DEFAULT GETDATE(),
    Weight DECIMAL(5, 2),
    Muscle_Mass DECIMAL(5, 2),
    Body_Fat_Percentage DECIMAL(5, 2),
    Notes NVARCHAR(MAX),
    FOREIGN KEY (Member_ID) REFERENCES Member(Member_ID),
    FOREIGN KEY (Trainer_ID) REFERENCES Trainer(Trainer_ID),
	PRIMARY KEY (Progress_ID)
);

CREATE TABLE Feedback (
    Feedback_ID INT IDENTITY(1,1),
    Member_ID INT,
    Message NVARCHAR(MAX),
    Type NVARCHAR(100),
    Date_Submitted DATE DEFAULT GETDATE(),
    Trainer_ID INT,
    Admin_ID INT,
    FOREIGN KEY (Member_ID) REFERENCES Member(Member_ID),
    FOREIGN KEY (Trainer_ID) REFERENCES Trainer(Trainer_ID),
    FOREIGN KEY (Admin_ID) REFERENCES Admin(Admin_ID),
	PRIMARY KEY (Feedback_ID)
);

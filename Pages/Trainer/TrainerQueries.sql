
SELECT DISTINCT
    M.Member_ID,
    M.Name,               
    M.Email,           
    M.Goal                
FROM
    Member M
JOIN
    Class_Enrollment CE ON M.Member_ID = CE.Member_ID
WHERE
    CE.Trainer_ID = @TrainerID;




SELECT
    Name
FROM
    Member
WHERE
    Member_ID = @MemberID;





INSERT INTO Workout_Plan (
    Member_ID, 
    Trainer_ID, 
    Weekly_Schedule, 
    Focus_Area, 
    Last_Updated
)
VALUES (
    @MemberID, 
    @TrainerID, 
    @WeeklySchedule, 
    @FocusArea,       
    GETDATE()
);



SELECT 
    Date_Recorded, 
    Weight, 
    Muscle_Mass, 
    Body_Fat_Percentage, 
    Notes
FROM 
    Progress
WHERE 
    Member_ID = @MemberID
ORDER BY 
    Date_Recorded DESC;



INSERT INTO Progress (
    Member_ID, 
    Trainer_ID, 
    Date_Recorded, 
    Weight, 
    Muscle_Mass, 
    Body_Fat_Percentage, 
    Notes
)
VALUES (
    @MemberID, 
    @TrainerID, 
    GETDATE(), 
    @Weight, 
    @MuscleMass,       
    @BodyFatPercentage, 
    @Notes
);
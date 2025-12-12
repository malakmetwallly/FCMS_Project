SELECT DISTINCT m.Member_ID, m.Name, m.Age, m.Goal
FROM Member m
JOIN Class_Enrollment ce ON m.Member_ID = ce.Member_ID
WHERE ce.Trainer_ID = @Trainer_ID;




INSERT INTO Workout_Plan (Member_ID, Trainer_ID, Weekly_Schedule, Focus_Area, Last_Updated)
VALUES (@Member_ID, @Trainer_ID, @Weekly_Schedule, @Focus_Area, GETDATE());

SELECT Plan_ID, Weekly_Schedule, Focus_Area, Last_Updated
FROM Workout_Plan
WHERE Member_ID = @Member_ID
ORDER BY Last_Updated DESC;




INSERT INTO Diet_Plan (Member_ID, Trainer_ID, Daily_Calories, Water_Intake_Goal, Allergies, Last_Updated)
VALUES (@Member_ID, @Trainer_ID, @Daily_Calories, @Water_Intake_Goal, @Allergies, GETDATE());

SELECT DietPlan_ID, Daily_Calories, Water_Intake_Goal, Allergies, Last_Updated
FROM Diet_Plan
WHERE Member_ID = @Member_ID
ORDER BY Last_Updated DESC;




INSERT INTO Progress (Member_ID, Trainer_ID, Weight, Muscle_Mass, Body_Fat_Percentage, Notes)
VALUES (@Member_ID, @Trainer_ID, @Weight, @Muscle_Mass, @Body_Fat_Percentage, @Notes);

SELECT Date_Recorded, Weight, Muscle_Mass, Body_Fat_Percentage, Notes
FROM Progress
WHERE Member_ID = @Member_ID
ORDER BY Date_Recorded;




INSERT INTO Feedback (Member_ID, Message, Type, Trainer_ID, Admin_ID)
VALUES (@Member_ID, @Message, @Type, @Trainer_ID, NULL);

SELECT Message, Type, Date_Submitted
FROM Feedback
WHERE Member_ID = @Member_ID
ORDER BY Date_Submitted DESC;

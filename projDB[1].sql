CREATE DATABASE PROJ_2
use PROJ_2

-- Table for ADMIN
CREATE TABLE ADMIN (
    Admin_ID INT PRIMARY KEY,
    Admin_Name VARCHAR(100),
    Email VARCHAR(100),
    Phone_Number VARCHAR(20),
    Admin_Password VARCHAR(20)
);

-- Table for MEMBERS_RECORD
CREATE TABLE MEMBERS_RECORD (
    Record_ID INT PRIMARY KEY,
    Membership_Duration INT,
    Payment VARCHAR(50),
    Membership_Type VARCHAR(50)
);

-- Table for MEMBER_WORKOUT_PLAN
CREATE TABLE MEMBER_WORKOUT_PLAN (
    Plan_ID INT PRIMARY KEY,
    Plan_Name VARCHAR(100),
    Member_ID INT,
    Exercise_ID INT,
    Rest_Intervals INT,
    Workout_Goals VARCHAR(255),
    FOREIGN KEY (Member_ID) REFERENCES MEMBERS(Member_ID) ON DELETE CASCADE,
    FOREIGN KEY (Exercise_ID) REFERENCES EXERCISE(Exercise_ID) ON DELETE CASCADE
);

-- Table for TRAINER_WORKOUT_PLAN
CREATE TABLE TRAINER_WORKOUT_PLAN (
    Plan_ID INT PRIMARY KEY,
    Plan_Name VARCHAR(100),
    Trainer_ID INT,
    Exercise_ID INT,
    Rest_Intervals INT,
    Workout_Goals VARCHAR(255),
    FOREIGN KEY (Trainer_ID) REFERENCES TRAINERS(Trainer_ID),
    FOREIGN KEY (Exercise_ID) REFERENCES EXERCISE(Exercise_ID)
);

-- Table for MEMBERS
CREATE TABLE MEMBERS (
    Member_ID INT PRIMARY KEY,
    Member_Name VARCHAR(100),
    Email VARCHAR(100),
    Phone_Number VARCHAR(20),
    Record_ID INT,
    Member_Password VARCHAR(20),
    FOREIGN KEY (Record_ID) REFERENCES MEMBERS_RECORD(Record_ID) ON DELETE CASCADE
);

-- Table for ALLERGIES
CREATE TABLE ALLERGIES (
    Allergies_ID INT PRIMARY KEY,
    Member_ID INT,
    Triggers VARCHAR(100),
    FOREIGN KEY (Member_ID) REFERENCES MEMBERS(Member_ID) ON DELETE CASCADE
);

-- Table for MACHINES
CREATE TABLE MACHINES (
    Machine_ID INT PRIMARY KEY,
    Machine_Name VARCHAR(100),
    Availability VARCHAR(50)
);

-- Table for EXERCISE
CREATE TABLE EXERCISE (
    Exercise_ID INT PRIMARY KEY,
    Exercise_Name VARCHAR(100),
    Muscle_Group VARCHAR(100),
    Machine_ID INT,
    Difficulty_Level VARCHAR(50),
    Experience_Level_Required VARCHAR(50),
    FOREIGN KEY (Machine_ID) REFERENCES MACHINES(Machine_ID) ON DELETE CASCADE
);

-- Table for OWNER
CREATE TABLE OWNER (
    Owner_ID INT PRIMARY KEY,
    Owner_Name VARCHAR(100),
    Email VARCHAR(100),
    Phone_Number VARCHAR(20),
    Owner_Password VARCHAR(20)
);

-- Table for GYM
CREATE TABLE GYM (
    Gym_ID INT PRIMARY KEY,
    Gym_Name VARCHAR(100),
    Location VARCHAR(100),
    Registration_Status VARCHAR(50),
    Owner_ID INT,
    FOREIGN KEY (Owner_ID) REFERENCES OWNER(Owner_ID) ON DELETE CASCADE
);

-- Table for TRAINERS
CREATE TABLE TRAINERS (
    Trainer_ID INT PRIMARY KEY,
    Trainer_Name VARCHAR(100),
    Gym_ID INT,
    Email VARCHAR(100),
    Phone_Number VARCHAR(20),
    Specialization VARCHAR(100),
    Trainer_Password VARCHAR(20),
    FOREIGN KEY (Gym_ID) REFERENCES GYM(Gym_ID) ON DELETE CASCADE
);

CREATE TABLE RATING_NEW (
    Rating_ID INT PRIMARY KEY,
    User_ID INT,
    Trainer_ID INT,
    Rating_Score INT,
    Feedback_Comments VARCHAR(255),
    FOREIGN KEY (User_ID) REFERENCES MEMBERS(Member_ID) ON DELETE CASCADE,
    FOREIGN KEY (Trainer_ID) REFERENCES TRAINERS(Trainer_ID) ON DELETE CASCADE
);

CREATE TABLE LOGIN_LOG_MEMBERS (
    Login_ID INT PRIMARY KEY IDENTITY(1,1),
    User_ID INT,
    FOREIGN KEY (User_ID) REFERENCES MEMBERS(Member_ID) ON DELETE CASCADE
);

-- Table for PERFORMANCE
CREATE TABLE PERFORMANCE (
    Performance_ID INT PRIMARY KEY,
    Gym_ID INT,
    Membership_Growth INT,
    Financial_Performance INT,
    Class_Attendance INT,
    Customer_Satisfaction INT,
    FOREIGN KEY (Gym_ID) REFERENCES GYM(Gym_ID) ON DELETE CASCADE
);

-- Table for MEALS
CREATE TABLE MEALS (
    Meal_ID INT PRIMARY KEY,
    Meal_Name VARCHAR(100),
    Nutrition_Info VARCHAR(255)
);

-- Table for NUTRITIONAL_INFO
CREATE TABLE NUTRITIONAL_INFO (
    Meal_ID INT PRIMARY KEY,
    Calories INT,
    Proteins INT,
    Carbohydrates INT,
    Fats INT,
    Fibres INT,
    FOREIGN KEY (Meal_ID) REFERENCES MEALS(Meal_ID) ON DELETE CASCADE
);

-- Table for MEAL_ALLERGENS
CREATE TABLE MEAL_ALLERGENS (
    Meal_ID INT,
    Allergens VARCHAR(100),
    PRIMARY KEY (Meal_ID, Allergens),
    FOREIGN KEY (Meal_ID) REFERENCES MEALS(Meal_ID) ON DELETE CASCADE
);

-- Table for TRAINING_SESSION
CREATE TABLE TRAINING_SESSION (
    Session_ID INT PRIMARY KEY,
    Trainer_ID INT,
    Member_ID INT,
    Session_Date DATE,
    Session_Time TIME,
    Duration INT,
    FOREIGN KEY (Trainer_ID) REFERENCES TRAINERS(Trainer_ID) ON DELETE CASCADE,
    FOREIGN KEY (Member_ID) REFERENCES MEMBERS(Member_ID) ON DELETE CASCADE
);

-- Table for DIET_PLAN
CREATE TABLE DIET_PLAN (
    Diet_Plan_ID INT PRIMARY KEY,
    Plan_Name VARCHAR(100),
    Member_ID INT,
    Trainer_ID INT,
    Meal_ID INT,
    Diet_Goal VARCHAR(100),
    FOREIGN KEY (Member_ID) REFERENCES MEMBERS(Member_ID) ON DELETE CASCADE,
    FOREIGN KEY (Trainer_ID) REFERENCES TRAINERS(Trainer_ID) ON DELETE CASCADE,
    FOREIGN KEY (Meal_ID) REFERENCES MEALS(Meal_ID) ON DELETE CASCADE
);

-- Table for DIET_PLAN_MEAL_LIST
CREATE TABLE DIET_PLAN_MEAL_LIST (
    Diet_Plan_ID INT,
    Meal_List VARCHAR(255),
    PRIMARY KEY (Diet_Plan_ID, Meal_List),
    FOREIGN KEY (Diet_Plan_ID) REFERENCES DIET_PLAN(Diet_Plan_ID) ON DELETE CASCADE
);

-- Table for GYM_REQUESTS
CREATE TABLE GYM_REQUESTS (
    Request_ID INT PRIMARY KEY,
    Gym_Name VARCHAR(100),
    Location VARCHAR(100),
    Owner_Name VARCHAR(100),
    Request_Status VARCHAR(50)
);

-- Table for MEM_CREATES_PLANS
CREATE TABLE MEM_CREATES_PLANS (
    Meal_ID INT,
    Plan_ID INT,
    PRIMARY KEY (Meal_ID, Plan_ID),
    FOREIGN KEY (Meal_ID) REFERENCES MEALS(Meal_ID) ON DELETE CASCADE,
    FOREIGN KEY (Plan_ID) REFERENCES MEMBER_WORKOUT_PLAN(Plan_ID) ON DELETE CASCADE
);

-- Table for CREATES_CUSTOMISE_PLAN
CREATE TABLE TRAINER_CUSTOMISE_WORKOUTPLAN (
    Trainer_ID INT,
    Plan_ID INT,
    PRIMARY KEY (Trainer_ID, Plan_ID),
    FOREIGN KEY (Trainer_ID) REFERENCES TRAINERS(Trainer_ID) ON DELETE CASCADE,
    FOREIGN KEY (Plan_ID) REFERENCES TRAINER_WORKOUT_PLAN(Plan_ID) ON DELETE NO ACTION
);

-- Table for CREATES_CUSTOMISEZ
CREATE TABLE CREATES_CUSTOMISEZ (
    Diet_Plan_ID INT,
    Trainer_ID INT,
    PRIMARY KEY (Diet_Plan_ID, Trainer_ID),
    FOREIGN KEY (Diet_Plan_ID) REFERENCES DIET_PLAN(Diet_Plan_ID)ON DELETE NO ACTION,
    FOREIGN KEY (Trainer_ID) REFERENCES TRAINERS(Trainer_ID) ON DELETE CASCADE
);

CREATE TABLE MEMBER_CONSISTS_OF (
    Exercise_ID INT,
    Plan_ID INT,
    PRIMARY KEY (Exercise_ID, Plan_ID),
    FOREIGN KEY (Exercise_ID) REFERENCES EXERCISE(Exercise_ID) ON DELETE NO ACTION,
    FOREIGN KEY (Plan_ID) REFERENCES MEMBER_WORKOUT_PLAN(Plan_ID) ON DELETE CASCADE
);

CREATE TABLE TRAINER_CONSISTS_OF (
    Exercise_ID INT,
    Plan_ID INT,
    PRIMARY KEY (Exercise_ID, Plan_ID),
    FOREIGN KEY (Exercise_ID) REFERENCES EXERCISE(Exercise_ID) ON DELETE CASCADE,
    FOREIGN KEY (Plan_ID) REFERENCES TRAINER_WORKOUT_PLAN(Plan_ID) ON DELETE CASCADE
);

-- Table for EXERCISE_USES_MACHINES
CREATE TABLE EXERCISE_USES_MACHINES (
    Machine_ID INT,
    Exercise_ID INT,
    Minutes INT,
    PRIMARY KEY (Machine_ID, Exercise_ID),
    FOREIGN KEY (Machine_ID) REFERENCES MACHINES(Machine_ID) ON DELETE CASCADE,
    FOREIGN KEY (Exercise_ID) REFERENCES EXERCISE(Exercise_ID)ON DELETE NO ACTION
);

CREATE TABLE LOGIN_LOG_TRAINERS (
    TrainerLogin_ID INT PRIMARY KEY IDENTITY(1,1),
    User_ID INT,
    FOREIGN KEY (User_ID) REFERENCES TRAINERS(Trainer_ID) ON DELETE CASCADE
);



-----------------------INSERTING VALUES------------------------------------------
-- Inserting dummy data into ADMIN table
INSERT INTO ADMIN (Admin_ID, Admin_Name, Email, Phone_Number, Admin_Password) 
VALUES 
(1, 'Admin1', 'admin1@example.com', '1111111111', 'adminpass1'),
(2, 'Admin2', 'admin2@example.com', '2222222222', 'adminpass2'),
(3, 'Admin3', 'admin3@example.com', '3333333333', 'adminpass3'),
(4, 'Admin4', 'admin4@example.com', '4444444444', 'adminpass4'),
(5, 'Admin5', 'admin5@example.com', '5555555555', 'adminpass5'),
(6, 'Admin6', 'admin6@example.com', '6666666666', 'adminpass6'),
(7, 'Admin7', 'admin7@example.com', '7777777777', 'adminpass7'),
(8, 'Admin8', 'admin8@example.com', '8888888888', 'adminpass8'),
(9, 'Admin9', 'admin9@example.com', '9999999999', 'adminpass9'),
(10, 'Admin10', 'admin10@example.com', '0000000000', 'adminpass10');


-- Inserting dummy data into MEMBERS_RECORD table
INSERT INTO MEMBERS_RECORD (Record_ID, Membership_Duration, Payment, Membership_Type) 
VALUES 
(1, 12, 'Monthly', 'Standard'),
(2, 6, 'Quarterly', 'Premium'),
(3, 3, 'Annually', 'Standard'),
(4, 12, 'Monthly', 'Premium'),
(5, 6, 'Quarterly', 'Standard'),
(6, 3, 'Annually', 'Premium'),
(7, 12, 'Monthly', 'Standard'),
(8, 6, 'Quarterly', 'Premium'),
(9, 3, 'Annually', 'Standard'),
(10, 12, 'Monthly', 'Premium');

-- Inserting dummy data into MEMBERS table
INSERT INTO MEMBERS (Member_ID, Member_Name, Email, Phone_Number, Record_ID, Member_Password) 
VALUES 
(1, 'Alice Johnson', 'alice.johnson@example.com', '1111111111', 1, 'memberpass1'),
(2, 'Bob Smith', 'bob.smith@example.com', '2222222222', 2, 'memberpass2'),
(3, 'Charlie Brown', 'charlie.brown@example.com', '3333333333', 3, 'memberpass3'),
(4, 'Diana Davis', 'diana.davis@example.com', '4444444444', 4, 'memberpass4'),
(5, 'Ella Wilson', 'ella.wilson@example.com', '5555555555', 5, 'memberpass5'),
(6, 'Frank Taylor', 'frank.taylor@example.com', '6666666666', 6, 'memberpass6'),
(7, 'Grace Martinez', 'grace.martinez@example.com', '7777777777', 7, 'memberpass7'),
(8, 'Henry Lee', 'henry.lee@example.com', '8888888888', 8, 'memberpass8'),
(9, 'Isabel Harris', 'isabel.harris@example.com', '9999999999', 9, 'memberpass9'),
(10, 'Jack Clark', 'jack.clark@example.com', '0000000000', 10, 'memberpass10');



iNSERT INTO TRAINER_CONSISTS_OF (Exercise_ID, Plan_ID)
VALUES
(1, 11),
(2, 11),
(3, 11),
(4, 12),
(5, 12),
(6, 13),
(7, 13),
(8, 13),
(9, 14),
(10, 14);

INSERT INTO MEMBER_CONSISTS_OF (Exercise_ID, Plan_ID)
VALUES
(1, 1),
(2, 1),
(3, 1),
(4, 2),
(5, 2),
(6, 3),
(7, 3),
(8, 3),
(9, 4),
(10, 4);

INSERT INTO TRAINER_WORKOUT_PLAN (Plan_ID, Plan_Name, Trainer_ID, Exercise_ID, Rest_Intervals, Workout_Goals)
VALUES
(11, 'Trainer Plan 1', 1, 1, 60, 'Build muscle strength'),
(12, 'Trainer Plan 2', 2, 2, 45, 'Increase endurance'),
(13, 'Trainer Plan 3', 3, 3, 30, 'Improve flexibility'),
(14, 'Trainer Plan 4', 4, 4, 60, 'Enhance agility'),
(15, 'Trainer Plan 5', 5, 5, 45, 'Cardiovascular fitness'),
(16, 'Trainer Plan 6', 6, 6, 30, 'Weight loss'),
(17, 'Trainer Plan 7', 7, 7, 60, 'Muscle toning'),
(18, 'Trainer Plan 8', 8, 8, 45, 'Strength and conditioning'),
(19, 'Trainer Plan 9', 9, 9, 30, 'High-intensity interval training'),
(20, 'Trainer Plan 10', 10, 10, 60, 'Overall fitness');

INSERT INTO MEMBER_WORKOUT_PLAN (Plan_ID, Plan_Name, Member_ID, Exercise_ID, Rest_Intervals, Workout_Goals)
VALUES
(1, 'Plan 1', 1, 1, 60, 'Build muscle strength'),
(2, 'Plan 2', 2, 2, 45, 'Increase endurance'),
(3, 'Plan 3', 3, 3, 30, 'Improve flexibility'),
(4, 'Plan 4', 4, 4, 60, 'Enhance agility'),
(5, 'Plan 5', 5, 5, 45, 'Cardiovascular fitness'),
(6, 'Plan 6', 6, 6, 30, 'Weight loss'),
(7, 'Plan 7', 7, 7, 60, 'Muscle toning'),
(8, 'Plan 8', 8, 8, 45, 'Strength and conditioning'),
(9, 'Plan 9', 9, 9, 30, 'High-intensity interval training'),
(10, 'Plan 10', 10, 10, 60, 'Overall fitness');


-- Inserting dummy data into ALLERGIES table
INSERT INTO ALLERGIES (Allergies_ID, Member_ID, Triggers) 
VALUES 
(1, 1, 'Peanuts'),
(2, 2, 'Shellfish'),
(3, 3, 'Dairy'),
(4, 4, 'Gluten'),
(5, 5, 'Soy'),
(6, 6, 'Eggs'),
(7, 7, 'Nuts'),
(8, 8, 'Fish'),
(9, 9, 'Wheat'),
(10, 10, 'Sesame Seeds');

--INSERTING INTO GYM REQUESTS
-- Inserting dummy values into the GYM_REQUESTS table
INSERT INTO GYM_REQUESTS (Request_ID, Gym_Name, Location, Owner_Name, Request_Status)
VALUES 
    (1, 'Fitness Second', '123 Main Street', 'John Doe', 'Pending'),
    (2, 'Gym Nationwide', '456 Oak Avenue', 'Jane Smith', 'Pending'),
    (3, 'Powerhouse', '789 Elm Street', 'Michael Johnson', 'Pending');


-- Inserting dummy data into MACHINES table
INSERT INTO MACHINES (Machine_ID, Machine_Name, Availability) 
VALUES 
(1, 'Smith Machine', 'Available'),
(2, 'Treadmill', 'Available'),
(3, 'Yoga Mat', 'Available'),
(4, 'Boxing Bag', 'Available'),
(5, 'Ab Roller', 'Available'),
(6, 'Jump Rope', 'Available'),
(7, 'Stereo System', 'Available'),
(8, 'Punching Bag', 'Available'),
(9, 'Exercise Bike', 'Available'),
(10, 'Leg Press Machine', 'Available');

-- Inserting dummy data into EXERCISE table
INSERT INTO EXERCISE (Exercise_ID, Exercise_Name, Muscle_Group, Machine_ID, Difficulty_Level, Experience_Level_Required) 
VALUES 
(1, 'Bench Press', 'Chest', 1, 'Intermediate', 'Intermediate'),
(2, 'Treadmill Running', 'Cardio', 2, 'Beginner', 'Beginner'),
(3, 'Downward Dog', 'Flexibility', 3, 'Beginner', 'Beginner'),
(4, 'Burpees', 'Full Body', 4, 'Advanced', 'Advanced'),
(5, 'Crunches', 'Core', 5, 'Beginner', 'Beginner'),
(6, 'Jumping Jacks', 'Cardio', 6, 'Beginner', 'Beginner'),
(7, 'Salsa Dancing', 'Cardio', 7, 'Intermediate', 'Intermediate'),
(8, 'Punching Bag', 'Cardio', 8, 'Intermediate', 'Intermediate'),
(9, 'Stationary Bike', 'Cardio', 9, 'Beginner', 'Beginner'),
(10, 'Squats', 'Legs', 10, 'Intermediate', 'Intermediate');


-- Inserting dummy data into OWNER table
INSERT INTO OWNER (Owner_ID, Owner_Name, Email, Phone_Number, Owner_Password) 
VALUES 
(1, 'Richard Johnson', 'richard.johnson@example.com', '1111111111', 'ownerpass1'),
(2, 'Emma Williams', 'emma.williams@example.com', '2222222222', 'ownerpass2'),
(3, 'Daniel Brown', 'daniel.brown@example.com', '3333333333', 'ownerpass3'),
(4, 'Olivia Jones', 'olivia.jones@example.com', '4444444444', 'ownerpass4'),
(5, 'William Davis', 'william.davis@example.com', '5555555555', 'ownerpass5'),
(6, 'Sophia Miller', 'sophia.miller@example.com', '6666666666', 'ownerpass6'),
(7, 'Matthew Wilson', 'matthew.wilson@example.com', '7777777777', 'ownerpass7'),
(8, 'Isabella Taylor', 'isabella.taylor@example.com', '8888888888', 'ownerpass8'),
(9, 'Joseph Anderson', 'joseph.anderson@example.com', '9999999999', 'ownerpass9'),
(10, 'Emily Thomas', 'emily.thomas@example.com', '0000000000', 'ownerpass10');

-- Inserting dummy data into GYM table
INSERT INTO GYM (Gym_ID, Gym_Name, Location, Registration_Status, Owner_ID) 
VALUES 
(1, 'Fitness First', 'New York', 'Active', 1),
(2, 'Gold''s Gym', 'Los Angeles', 'Active', 2),
(3, '24 Hour Fitness', 'Chicago', 'Active', 3),
(4, 'Anytime Fitness', 'Houston', 'Active', 4),
(5, 'Planet Fitness', 'Miami', 'Active', 5),
(6, 'LA Fitness', 'San Francisco', 'Active', 6),
(7, 'Equinox', 'Boston', 'Active', 7),
(8, 'Crunch Fitness', 'Seattle', 'Active', 8),
(9, 'GoodLife Fitness', 'Toronto', 'Active', 9),
(10, 'Snap Fitness', 'London', 'Active', 10);

-- Inserting dummy data into TRAINERS table
INSERT INTO TRAINERS (Trainer_ID, Trainer_Name, Gym_ID, Email, Phone_Number, Specialization, Trainer_Password) 
VALUES 
(1, 'John Doe', 1, 'john.doe@example.com', '1234567890', 'Strength Training', 'password1'),
(2, 'Jane Smith', 2, 'jane.smith@example.com', '0987654321', 'Cardio', 'password2'),
(3, 'Michael Johnson', 3, 'michael.johnson@example.com', '5555555555', 'Yoga', 'password3'),
(4, 'Emily Brown', 4, 'emily.brown@example.com', '6666666666', 'CrossFit', 'password4'),
(5, 'David Wilson', 5, 'david.wilson@example.com', '7777777777', 'Pilates', 'password5'),
(6, 'Sarah Lee', 6, 'sarah.lee@example.com', '8888888888', 'HIIT', 'password6'),
(7, 'Chris Taylor', 7, 'chris.taylor@example.com', '9999999999', 'Zumba', 'password7'),
(8, 'Amanda Martinez', 8, 'amanda.martinez@example.com', '4444444444', 'Kickboxing', 'password8'),
(9, 'James Adams', 9, 'james.adams@example.com', '3333333333', 'Spinning', 'password9'),
(10, 'Jessica White', 10, 'jessica.white@example.com', '2222222222', 'Functional Training', 'password10');



INSERT INTO RATING_NEW (Rating_ID, User_ID, Trainer_ID, Rating_Score, Feedback_Comments)
VALUES
(1, 1, 1, 4, 'Great trainer, very knowledgeable.'),
(2, 2, 2, 5, 'Excellent session, highly recommended.'),
(3, 3, 3, 3, 'Average workout, could be better.'),
(4, 4, 4, 4, 'Friendly trainer, good communication.'),
(5, 5, 5, 5, 'Best trainer ever, helped me achieve my goals.'),
(6, 6, 6, 2, 'Not satisfied with the training, lack of attention.'),
(7, 7, 7, 4, 'Effective workout, saw improvements.'),
(8, 8, 8, 5, 'Outstanding trainer, very motivating.'),
(9, 9, 9, 3, 'Decent session, room for improvement.'),
(10, 10, 10, 4, 'Overall good experience, would train again.');


-- Inserting dummy data into PERFORMANCE table
INSERT INTO PERFORMANCE (Performance_ID, Gym_ID, Membership_Growth, Financial_Performance, Class_Attendance, Customer_Satisfaction) 
VALUES 
(1, 1, 10, 90, 85, 95),
(2, 2, 15, 85, 80, 90),
(3, 3, 8, 95, 90, 85),
(4, 4, 12, 80, 75, 80),
(5, 5, 20, 75, 70, 85),
(6, 6, 10, 85, 80, 90),
(7, 7, 15, 90, 85, 95),
(8, 8, 8, 95, 90, 85),
(9, 9, 12, 80, 75, 80),
(10, 10, 20, 75, 70, 85);


-- Inserting dummy data into MEALS table
INSERT INTO MEALS (Meal_ID, Meal_Name, Nutrition_Info) 
VALUES 
(1, 'Grilled Chicken Salad', 'Calories: 300, Proteins: 25g, Carbohydrates: 10g, Fats: 15g, Fibres: 5g'),
(2, 'Salmon with Quinoa', 'Calories: 400, Proteins: 30g, Carbohydrates: 20g, Fats: 20g, Fibres: 8g'),
(3, 'Vegetable Stir-Fry', 'Calories: 250, Proteins: 15g, Carbohydrates: 30g, Fats: 10g, Fibres: 10g'),
(4, 'Turkey and Avocado Wrap', 'Calories: 350, Proteins: 20g, Carbohydrates: 25g, Fats: 15g, Fibres: 6g'),
(5, 'Tofu and Broccoli Bowl', 'Calories: 300, Proteins: 20g, Carbohydrates: 25g, Fats: 15g, Fibres: 8g'),
(6, 'Oatmeal with Berries', 'Calories: 250, Proteins: 10g, Carbohydrates: 40g, Fats: 5g, Fibres: 10g'),
(7, 'Greek Yogurt with Honey', 'Calories: 200, Proteins: 15g, Carbohydrates: 20g, Fats: 5g, Fibres: 2g'),
(8, 'Egg and Spinach Breakfast Wrap', 'Calories: 300, Proteins: 20g, Carbohydrates: 25g, Fats: 15g, Fibres: 5g'),
(9, 'Quinoa and Black Bean Salad', 'Calories: 350, Proteins: 25g, Carbohydrates: 30g, Fats: 10g, Fibres: 8g'),
(10, 'Shrimp and Vegetable Skewers', 'Calories: 300, Proteins: 20g, Carbohydrates: 15g, Fats: 15g, Fibres: 6g');

-- Inserting dummy data into NUTRITIONAL_INFO table
INSERT INTO NUTRITIONAL_INFO (Meal_ID, Calories, Proteins, Carbohydrates, Fats, Fibres) 
VALUES 
(1, 300, 25, 10, 15, 5),
(2, 400, 30, 20, 20, 8),
(3, 250, 15, 30, 10, 10),
(4, 350, 20, 25, 15, 6),
(5, 300, 20, 25, 15, 8),
(6, 250, 10, 40, 5, 10),
(7, 200, 15, 20, 5, 2),
(8, 300, 20, 25, 15, 5),
(9, 350, 25, 30, 10, 8),
(10, 300, 20, 15, 15, 6);

-- Inserting dummy data into MEAL_ALLERGENS table
INSERT INTO MEAL_ALLERGENS (Meal_ID, Allergens) 
VALUES 
(1, 'Nuts'),
(2, 'Fish'),
(3, 'Dairy'),
(4, 'Gluten'),
(5, 'Soy'),
(6, 'Eggs'),
(7, 'Dairy'),
(8, 'Eggs'),
(9, 'Nuts'),
(10, 'Shellfish');

-- Inserting dummy data into TRAINING_SESSION table
INSERT INTO TRAINING_SESSION (Session_ID, Trainer_ID, Member_ID, Session_Date, Session_Time, Duration) 
VALUES 
(1, 1, 1, '2024-05-01', '09:00:00', 60),
(2, 2, 2, '2024-05-02', '10:00:00', 45),
(3, 3, 3, '2024-05-03', '11:00:00', 60),
(4, 4, 4, '2024-05-04', '12:00:00', 60),
(5, 5, 5, '2024-05-05', '13:00:00', 45),
(6, 6, 6, '2024-05-06', '14:00:00', 60),
(7, 7, 7, '2024-05-07', '15:00:00', 60),
(8, 8, 8, '2024-05-08', '16:00:00', 45),
(9, 9, 9, '2024-05-09', '17:00:00', 60),
(10, 10, 10, '2024-05-10', '18:00:00', 60);

-- Inserting dummy data into DIET_PLAN table
INSERT INTO DIET_PLAN (Diet_Plan_ID, Plan_Name, Member_ID, Trainer_ID, Meal_ID, Diet_Goal) 
VALUES 
(1, 'Healthy Eating', 1, 1, 1, 'Weight loss'),
(2, 'Muscle Gain', 2, 2, 2, 'Increase muscle mass'),
(3, 'Vegetarian Diet', 3, 3, 3, 'Plant-based nutrition'),
(4, 'Gluten-Free Meal Plan', 4, 4, 4, 'Gluten intolerance'),
(5, 'Vegan Lifestyle', 5, 5, 5, 'Animal welfare'),
(6, 'Low-Carb Diet', 6, 6, 6, 'Reduce carbohydrates'),
(7, 'Balanced Nutrition', 7, 7, 7, 'Overall health'),
(8, 'Keto Diet', 8, 8, 8, 'Ketosis induction'),
(9, 'Paleo Diet', 9, 9, 9, 'Natural foods'),
(10, 'Intermittent Fasting', 10, 10, 10, 'Metabolic health');

-- Inserting dummy data into DIET_PLAN_MEAL_LIST table
INSERT INTO DIET_PLAN_MEAL_LIST (Diet_Plan_ID, Meal_List) 
VALUES 
(1, 'Breakfast, Lunch, Dinner'),
(2, 'Breakfast, Snack, Lunch, Dinner'),
(3, 'Breakfast, Lunch, Snack, Dinner'),
(4, 'Breakfast, Lunch, Dinner'),
(5, 'Breakfast, Snack, Lunch, Dinner'),
(6, 'Breakfast, Lunch, Dinner'),
(7, 'Breakfast, Snack, Lunch, Dinner'),
(8, 'Breakfast, Lunch, Dinner'),
(9, 'Breakfast, Snack, Lunch, Dinner'),
(10, 'Breakfast, Lunch, Dinner');

-- Inserting dummy data into MEM_CREATES_PLANS table
INSERT INTO MEM_CREATES_PLANS (Meal_ID, Plan_ID) 
VALUES 
(1, 1),
(2, 1),
(3, 1),
(4, 2),
(5, 2),
(6, 2),
(7, 3),
(8, 3),
(9, 3),
(10, 4);

-- Inserting dummy data into CREATES_CUSTOMISE_PLAN table
INSERT INTO CREATES_CUSTOMISE_PLAN (Trainer_ID, Session_ID) 
VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10);


-- Inserting dummy data into CREATES_CUSTOMISEZ table
INSERT INTO CREATES_CUSTOMISEZ (Diet_Plan_ID, Trainer_ID) 
VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10);
-- Inserting dummy data into EXERCISE_USES_MACHINES table
INSERT INTO EXERCISE_USES_MACHINES (Machine_ID, Exercise_ID, Minutes) 
VALUES 
(1, 1, 30),
(2, 2, 20),
(3, 3, 15),
(4, 4, 30),
(5, 5, 15),
(6, 6, 20),
(7, 7, 30),
(8, 8, 30),
(9, 9, 20),
(10, 10, 30);



SELECT * FROM ADMIN
--SELECT * FROM ALLERGIES
SELECT * FROM CREATES_CUSTOMISE_PLAN
SELECT * FROM CREATES_CUSTOMISEZ
--SELECT * FROM DIET_PLAN
--SELECT * FROM DIET_PLAN_MEAL_LIST
SELECT * FROM EXERCISE
SELECT * FROM EXERCISE_USES_MACHINES
SELECT * FROM GYM
SELECT * FROM MACHINES
--SELECT * FROM MEAL_ALLERGENS
--SELECT * FROM MEALS
--SELECT * FROM MEM_CREATES_PLANS
SELECT * FROM MEMBERS
--SELECT * FROM NUTRITIONAL_INFO
SELECT * FROM OWNER
SELECT * FROM PERFORMANCE
SELECT * FROM RATING_NEW
SELECT * FROM TRAINERS
SELECT * FROM TRAINING_SESSION
SELECT * FROM GYM_REQUESTS
SELECT * FROM TRAINER_WORKOUT_PLAN
SELECT * FROM TRAINER_CUSTOMISE_WORKOUTPLAN
SELECT * FROM MEMBER_WORKOUT_PLAN
SELECT * FROM MEMBER_DIET_PLAN_NEW
SELECT * FROM TRAINER_DIET_PLAN_NEW
SELECT * FROM ALLERGENS
SELECT * FROM MEALS_NEW
SELECT * FROM MEMBER_CONSISTS_OF_DIET_NEW2
SELECT * FROM TRAINER_CONSISTS_OF_DIET_NEW2
SELECT * FROM MEAL_ALLERGENS_NEW2

-- Table for CREATES_CUSTOMISE_PLAN
CREATE TABLE CREATES_CUSTOMISE_PLAN (
    Trainer_ID INT,
    Session_ID INT,
    PRIMARY KEY (Trainer_ID, Session_ID),
    FOREIGN KEY (Trainer_ID) REFERENCES TRAINERS(Trainer_ID) ON DELETE CASCADE,
    FOREIGN KEY (Session_ID) REFERENCES TRAINING_SESSION(Session_ID) ON DELETE NO ACTION
);

SELECT name
FROM sys.foreign_keys
WHERE parent_object_id = OBJECT_ID('CREATES_CUSTOMISE_PLAN');

SELECT name
FROM sys.foreign_keys
WHERE parent_object_id = OBJECT_ID('CREATES_CUSTOMISEZ');


ALTER TABLE CREATES_CUSTOMISE_PLAN
    DROP CONSTRAINT FK__CREATES_C__Sessi__65370702;

ALTER TABLE CREATES_CUSTOMISEZ
    DROP CONSTRAINT FK__CREATES_C__Diet___17036CC0; 

CREATE TRIGGER trg_InsertIntoTrainerCustomiseWorkoutPlan
ON TRAINER_WORKOUT_PLAN
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    -- Insert corresponding records into TRAINER_CUSTOMISE_WORKOUTPLAN
    INSERT INTO TRAINER_CUSTOMISE_WORKOUTPLAN (Trainer_ID, Plan_ID)
    SELECT i.Trainer_ID, i.Plan_ID
    FROM inserted i;
END;


SELECT t.Diet_Plan_ID, t.Plan_Name, t.Trainer_ID, t.Meal_ID, t.Diet_Goal, e.Meal_Name, t.Diet_Goal, m.Allergen_Name
FROM TRAINER_DIET_PLAN_NEW t
JOIN MEALS_NEW e ON t.Meal_ID = e.Meal_ID
JOIN ALLERGENS m ON e.Allergen_ID = m.Allergen_ID
WHERE e.Meal_Name = 'Breakfast'
AND t.Diet_Goal = 'Weight loss'
AND m.Allergen_Name = 'Peanuts';

ALTER TABLE MEMBERS_RECORD
ADD starting_date DATE;


CREATE TABLE MEMBER_DIET_PLAN_NEW (
    Diet_Plan_ID INT PRIMARY KEY,
    Plan_Name VARCHAR(100),
    Member_ID INT,
	Meal_ID INT,
    Diet_Goal VARCHAR(100),
    FOREIGN KEY (Member_ID) REFERENCES MEMBERS(Member_ID) ON DELETE CASCADE,
    FOREIGN KEY (Meal_ID) REFERENCES MEALS_NEW(Meal_ID) ON DELETE CASCADE
);


CREATE TABLE TRAINER_DIET_PLAN_NEW (
    Diet_Plan_ID INT PRIMARY KEY,
    Plan_Name VARCHAR(100),
    Trainer_ID INT,
	Meal_ID INT,
    Diet_Goal VARCHAR(100),
    FOREIGN KEY (Trainer_ID) REFERENCES TRAINERS(Trainer_ID) ON DELETE CASCADE,
    FOREIGN KEY (Meal_ID) REFERENCES MEALS_NEW(Meal_ID) ON DELETE CASCADE
);




CREATE TABLE ALLERGENS (
	Allergen_ID INT PRIMARY KEY,
	Allergen_Name VARCHAR(100)
);

-- Table for MEALS
CREATE TABLE MEALS_NEW (
    Meal_ID INT PRIMARY KEY,
    Meal_Name VARCHAR(100),
    Nutrition_Info VARCHAR(255),
	Allergen_ID INT,
	FOREIGN KEY (Allergen_ID) REFERENCES ALLERGENS(Allergen_ID) ON DELETE CASCADE
);


CREATE TABLE MEMBER_CONSISTS_OF_DIET_NEW2 (
    Meal_ID INT,
    Diet_Plan_ID INT,
    PRIMARY KEY (Meal_ID, Diet_Plan_ID),
    FOREIGN KEY (Meal_ID) REFERENCES MEALS_NEW(Meal_ID) ON DELETE NO ACTION,
    FOREIGN KEY (Diet_Plan_ID) REFERENCES MEMBER_DIET_PLAN_NEW(Diet_Plan_ID) ON DELETE CASCADE
);

CREATE TABLE TRAINER_CONSISTS_OF_DIET_NEW2 (
    Meal_ID INT,
    Diet_Plan_ID INT,
    PRIMARY KEY (Meal_ID, Diet_Plan_ID),
    FOREIGN KEY (Meal_ID) REFERENCES MEALS_NEW(Meal_ID) ON DELETE NO ACTION,
    FOREIGN KEY (Diet_Plan_ID) REFERENCES TRAINER_DIET_PLAN_NEW(Diet_Plan_ID) ON DELETE CASCADE
);



CREATE TABLE MEAL_ALLERGENS_NEW2 (
    Meal_ID INT,
    Allergen_ID INT,
    PRIMARY KEY (Meal_ID, Allergen_ID),
    FOREIGN KEY (Meal_ID) REFERENCES MEALS_NEW(Meal_ID) ,
    FOREIGN KEY (Allergen_ID) REFERENCES ALLERGENS(Allergen_ID)
);


INSERT INTO ALLERGENS (Allergen_ID, Allergen_Name) 
VALUES 
(1, 'Peanuts'),
(2, 'Shellfish'),
(3, 'Gluten'),
(4, 'Dairy'),
(5, 'Eggs'),
(6, 'Soy'),
(7, 'Fish'),
(8, 'Tree nuts'),
(9, 'Wheat'),
(10, 'Sesame');


INSERT INTO MEALS_NEW (Meal_ID, Meal_Name, Nutrition_Info, Allergen_ID) 
VALUES 
(1, 'Breakfast', 'Calories: 300, Protein: 15g, Carbs: 30g, Fat: 10g', 1),
(2, 'Lunch', 'Calories: 500, Protein: 25g, Carbs: 40g, Fat: 20g', 2),
(3, 'Dinner', 'Calories: 400, Protein: 20g, Carbs: 35g, Fat: 15g', 3),
(4, 'Snack', 'Calories: 200, Protein: 10g, Carbs: 15g, Fat: 5g', 4),
(5, 'Pre-workout', 'Calories: 250, Protein: 20g, Carbs: 25g, Fat: 8g', 5),
(6, 'Post-workout', 'Calories: 300, Protein: 30g, Carbs: 35g, Fat: 10g', 6),
(7, 'Dessert', 'Calories: 350, Protein: 5g, Carbs: 40g, Fat: 18g', 7),
(8, 'Snack 2', 'Calories: 150, Protein: 8g, Carbs: 10g, Fat: 3g', 8),
(9, 'Lunch 2', 'Calories: 550, Protein: 30g, Carbs: 45g, Fat: 25g', 9),
(10, 'Dinner 2', 'Calories: 450, Protein: 25g, Carbs: 40g, Fat: 20g', 10);


INSERT INTO MEMBER_DIET_PLAN_NEW (Diet_Plan_ID, Plan_Name, Member_ID, Meal_ID, Diet_Goal) 
VALUES 
(1, 'Plan 1', 1, 1, 'Weight loss'),
(2, 'Plan 2', 2, 2, 'Muscle gain'),
(3, 'Plan 3', 3, 3, 'Healthy living'),
(4, 'Plan 4', 4, 4, 'Low-carb diet'),
(5, 'Plan 5', 5, 5, 'Vegan diet'),
(6, 'Plan 6', 6, 6, 'Gluten-free diet'),
(7, 'Plan 7', 7, 7, 'Ketogenic diet'),
(8, 'Plan 8', 8, 8, 'Intermittent fasting'),
(9, 'Plan 9', 9, 9, 'Paleo diet'),
(10, 'Plan 10', 10, 10, 'Balanced diet');


INSERT INTO MEMBER_CONSISTS_OF_DIET_NEW2 (Meal_ID, Diet_Plan_ID) 
VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10);

INSERT INTO MEAL_ALLERGENS_NEW2 (Meal_ID, Allergen_ID)
VALUES
(1, 1),  -- Breakfast may contain Peanuts
(2, 2),  -- Lunch may contain Shellfish
(3, 3),  -- Dinner may contain Gluten
(4, 4),  -- Snack may contain Dairy
(5, 5),  -- Pre-workout may contain Eggs
(6, 6),  -- Post-workout may contain Soy
(7, 7),  -- Dessert may contain Fish
(8, 8),  -- Snack 2 may contain Tree nuts
(9, 9),  -- Lunch 2 may contain Wheat
(10, 10);  -- Dinner 2 may contain Sesame

INSERT INTO TRAINER_DIET_PLAN_NEW (Diet_Plan_ID, Plan_Name, Trainer_ID, Meal_ID, Diet_Goal) 
VALUES 
(1, 'Plan 1', 1, 1, 'Weight loss'),
(2, 'Plan 2', 2, 2, 'Muscle gain'),
(3, 'Plan 3', 3, 3, 'Healthy living'),
(4, 'Plan 4', 4, 4, 'Low-carb diet'),
(5, 'Plan 5', 5, 5, 'Vegan diet'),
(6, 'Plan 6', 6, 6, 'Gluten-free diet'),
(7, 'Plan 7', 7, 7, 'Ketogenic diet'),
(8, 'Plan 8', 8, 8, 'Intermittent fasting'),
(9, 'Plan 9', 9, 9, 'Paleo diet'),
(10, 'Plan 10', 10, 10, 'Balanced diet');

INSERT INTO TRAINER_CONSISTS_OF_DIET_NEW2 (Meal_ID, Diet_Plan_ID) 
VALUES 
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10);

-----------AUDIT TRAILS----------------

-- Table for Admin Audit Trail
-- Create audit trail table
CREATE TABLE AUDIT_TRAIL (
    Audit_ID INT PRIMARY KEY IDENTITY(1,1),
    TableName VARCHAR(100),
    Action VARCHAR(20),
    Old_Value VARCHAR(MAX),
    New_Value VARCHAR(MAX),
    Changed_By VARCHAR(100),
    Changed_At DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Trigger for auditing INSERT actions on a specific table (replace TableName with the actual name of your table)

--ADMIN
CREATE TRIGGER trg_audit_insert
ON ADMIN
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'ADMIN' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update
ON ADMIN
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'ADMIN' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete
ON ADMIN
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'ADMIN' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;



--ALLERGENS

CREATE TRIGGER trg_audit_insert_ALLERGENS
ON ALLERGENS
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'ALLERGENS' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_ALLERGENS
ON ALLERGENS
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'ALLERGENS' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_ALLERGENS
ON ALLERGENS
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'ALLERGENS' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;


--CREATES_CUSTOMISE_PLAN

CREATE TRIGGER trg_audit_insert_CREATES_CUSTOMISE_PLAN
ON CREATES_CUSTOMISE_PLAN
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'CREATES_CUSTOMISE_PLAN' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_CREATES_CUSTOMISE_PLAN
ON CREATES_CUSTOMISE_PLAN
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'CREATES_CUSTOMISE_PLAN' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_CREATES_CUSTOMISE_PLAN
ON CREATES_CUSTOMISE_PLAN
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'CREATES_CUSTOMISE_PLAN' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;


--CREATES_CUSTOMISEZ

CREATE TRIGGER trg_audit_insert_CREATES_CUSTOMISEZ
ON CREATES_CUSTOMISEZ
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'CREATES_CUSTOMISEZ' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_CREATES_CUSTOMISEZ
ON CREATES_CUSTOMISEZ
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'CREATES_CUSTOMISEZ' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_CREATES_CUSTOMISEZ
ON CREATES_CUSTOMISEZ
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'CREATES_CUSTOMISEZ' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;



--EXERCISE_USES_MACHINES

CREATE TRIGGER trg_audit_insert_EXERCISE_USES_MACHINES
ON EXERCISE_USES_MACHINES
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'EXERCISE_USES_MACHINES' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_EXERCISE_USES_MACHINES
ON EXERCISE_USES_MACHINES
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'EXERCISE_USES_MACHINES' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_EXERCISE_USES_MACHINES
ON EXERCISE_USES_MACHINES
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'EXERCISE_USES_MACHINES' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;


--EXERCISE

CREATE TRIGGER trg_audit_insert_EXERCISE
ON EXERCISE
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'EXERCISE' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_EXERCISE
ON EXERCISE
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'EXERCISE' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_EXERCISE
ON EXERCISE
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'EXERCISE' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;



--GYM

CREATE TRIGGER trg_audit_insert_GYM
ON GYM
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'GYM' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_GYM
ON GYM
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'GYM' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_GYM
ON GYM
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'GYM' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;



--GYM_REQUESTS

CREATE TRIGGER trg_audit_insert_GYM_REQUESTS
ON GYM_REQUESTS
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'GYM_REQUESTS' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_GYM_REQUESTS
ON GYM_REQUESTS
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'GYM_REQUESTS' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_GYM_REQUESTS
ON GYM_REQUESTS
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'GYM_REQUESTS' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;


--LOGIN_LOG_MEMBERS

CREATE TRIGGER trg_audit_insert_LOGIN_LOG_MEMBERS
ON LOGIN_LOG_MEMBERS
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'LOGIN_LOG_MEMBERS' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_LOGIN_LOG_MEMBERS
ON LOGIN_LOG_MEMBERS
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'LOGIN_LOG_MEMBERS' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_LOGIN_LOG_MEMBERS
ON LOGIN_LOG_MEMBERS
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'LOGIN_LOG_MEMBERS' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;



--MACHINES

CREATE TRIGGER trg_audit_insert_MACHINES
ON MACHINES
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'MACHINES' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_MACHINES
ON MACHINES
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MACHINES' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_MACHINES
ON MACHINES
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MACHINES' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;


--MEAL_ALLERGENS_NEW2

CREATE TRIGGER trg_audit_insert_MEAL_ALLERGENS_NEW2
ON MEAL_ALLERGENS_NEW2
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'MEAL_ALLERGENS_NEW2' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_MEAL_ALLERGENS_NEW2
ON MEAL_ALLERGENS_NEW2
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MEAL_ALLERGENS_NEW2' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_MEAL_ALLERGENS_NEW2
ON MEAL_ALLERGENS_NEW2
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MEAL_ALLERGENS_NEW2' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;



--MEALS_NEW

CREATE TRIGGER trg_audit_insert_MEALS_NEW
ON MEALS_NEW
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'MEALS_NEW' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_MEALS_NEW
ON MEALS_NEW
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MEALS_NEW' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_MEALS_NEW
ON MEALS_NEW
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MEALS_NEW' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;



--MEM_CREATES_PLANS

CREATE TRIGGER trg_audit_insert_MEM_CREATES_PLANS
ON MEM_CREATES_PLANS
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'MEM_CREATES_PLANS' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_MEM_CREATES_PLANS
ON MEM_CREATES_PLANS
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MEM_CREATES_PLANS' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_MEM_CREATES_PLANS
ON MEM_CREATES_PLANS
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MEM_CREATES_PLANS' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;


--MEMBER_CONSISTS_OF

CREATE TRIGGER trg_audit_insert_MEMBER_CONSISTS_OF
ON MEMBER_CONSISTS_OF
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'MEMBER_CONSISTS_OF' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_MEMBER_CONSISTS_OF
ON MEMBER_CONSISTS_OF
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MEMBER_CONSISTS_OF' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_MEMBER_CONSISTS_OF
ON MEMBER_CONSISTS_OF
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MEMBER_CONSISTS_OF' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;



--MEMBER_CONSISTS_OF_DIET_NEW2

CREATE TRIGGER trg_audit_insert_MEMBER_CONSISTS_OF_DIET_NEW2
ON MEMBER_CONSISTS_OF_DIET_NEW2
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'MEMBER_CONSISTS_OF_DIET_NEW2' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_MEMBER_CONSISTS_OF_DIET_NEW2
ON MEMBER_CONSISTS_OF_DIET_NEW2
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MEMBER_CONSISTS_OF_DIET_NEW2' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_MEMBER_CONSISTS_OF_DIET_NEW2
ON MEMBER_CONSISTS_OF_DIET_NEW2
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MEMBER_CONSISTS_OF_DIET_NEW2' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;


--MEMBER_DIET_PLAN_NEW

CREATE TRIGGER trg_audit_insert_MEMBER_DIET_PLAN_NEW
ON MEMBER_DIET_PLAN_NEW
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'MEMBER_DIET_PLAN_NEW' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_MEMBER_DIET_PLAN_NEW
ON MEMBER_DIET_PLAN_NEW
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MEMBER_DIET_PLAN_NEW' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_MEMBER_DIET_PLAN_NEW
ON MEMBER_DIET_PLAN_NEW
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MEMBER_DIET_PLAN_NEW' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;



--MEMBER_WORKOUT_PLAN

CREATE TRIGGER trg_audit_insert_MEMBER_WORKOUT_PLAN
ON MEMBER_WORKOUT_PLAN
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'MEMBER_WORKOUT_PLAN' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_MEMBER_WORKOUT_PLAN
ON MEMBER_WORKOUT_PLAN
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MEMBER_WORKOUT_PLAN' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_MEMBER_WORKOUT_PLAN
ON MEMBER_WORKOUT_PLAN
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MEMBER_WORKOUT_PLAN' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;


--MEMBERS

CREATE TRIGGER trg_audit_insert_MEMBERS
ON MEMBERS
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'MEMBERS' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_MEMBERS
ON MEMBERS
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MEMBERS' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_MEMBERS
ON MEMBERS
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MEMBERS' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;



--MEMBERS_RECORD

CREATE TRIGGER trg_audit_insert_MEMBERS_RECORD
ON MEMBERS_RECORD
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'MEMBERS_RECORD' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_MEMBERS_RECORD
ON MEMBERS_RECORD
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MEMBERS_RECORD' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_MEMBERS_RECORD
ON MEMBERS_RECORD
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'MEMBERS_RECORD' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;


--OWNER

CREATE TRIGGER trg_audit_insert_OWNER
ON OWNER
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'OWNER' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_OWNER
ON OWNER
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'OWNER' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_OWNER
ON OWNER
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'OWNER' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;



--PERFORMANCE

CREATE TRIGGER trg_audit_insert_PERFORMANCE
ON PERFORMANCE
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'PERFORMANCE' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_PERFORMANCE
ON PERFORMANCE
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'PERFORMANCE' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_PERFORMANCE
ON PERFORMANCE
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'PERFORMANCE' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;


--RATING_NEW

CREATE TRIGGER trg_audit_insert_RATING_NEW
ON RATING_NEW
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'RATING_NEW' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_RATING_NEW
ON RATING_NEW
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'RATING_NEW' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_RATING_NEW
ON RATING_NEW
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'RATING_NEW' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;




--TRAINER_CONSISTS_OF

CREATE TRIGGER trg_audit_insert_TRAINER_CONSISTS_OF
ON TRAINER_CONSISTS_OF
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'TRAINER_CONSISTS_OF' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_TRAINER_CONSISTS_OF
ON TRAINER_CONSISTS_OF
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'TRAINER_CONSISTS_OF' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_TRAINER_CONSISTS_OF
ON TRAINER_CONSISTS_OF
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'TRAINER_CONSISTS_OF' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;



--TRAINER_CONSISTS_OF_DIET_NEW2

CREATE TRIGGER trg_audit_insert_TRAINER_CONSISTS_OF_DIET_NEW2
ON TRAINER_CONSISTS_OF_DIET_NEW2
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'TRAINER_CONSISTS_OF_DIET_NEW2' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_TRAINER_CONSISTS_OF_DIET_NEW2
ON TRAINER_CONSISTS_OF_DIET_NEW2
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'TRAINER_CONSISTS_OF_DIET_NEW2' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_TRAINER_CONSISTS_OF_DIET_NEW2
ON TRAINER_CONSISTS_OF_DIET_NEW2
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'TRAINER_CONSISTS_OF_DIET_NEW2' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;



--TRAINER_DIET_PLAN_NEW

CREATE TRIGGER trg_audit_insert_TRAINER_DIET_PLAN_NEW
ON TRAINER_DIET_PLAN_NEW
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'TRAINER_DIET_PLAN_NEW' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_TRAINER_DIET_PLAN_NEW
ON TRAINER_DIET_PLAN_NEW
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'TRAINER_DIET_PLAN_NEW' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_TRAINER_DIET_PLAN_NEW
ON TRAINER_DIET_PLAN_NEW
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'TRAINER_DIET_PLAN_NEW' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;



--TRAINER_WORKOUT_PLAN

CREATE TRIGGER trg_audit_insert_TRAINER_WORKOUT_PLAN
ON TRAINER_WORKOUT_PLAN
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'TRAINER_WORKOUT_PLAN' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_TRAINER_WORKOUT_PLAN
ON TRAINER_WORKOUT_PLAN
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'TRAINER_WORKOUT_PLAN' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_TRAINER_WORKOUT_PLAN
ON TRAINER_WORKOUT_PLAN
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'TRAINER_WORKOUT_PLAN' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;



--TRAINERS

CREATE TRIGGER trg_audit_insert_TRAINERS
ON TRAINERS
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'TRAINERS' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_TRAINERS
ON TRAINERS
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'TRAINERS' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_TRAINERS
ON TRAINERS
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'TRAINERS' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;


--TRAINING_SESSION

CREATE TRIGGER trg_audit_insert_TRAINING_SESSION
ON TRAINING_SESSION
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, New_Value, Changed_By)
    SELECT 'TRAINING_SESSION' AS TableName, -- Replace 'TableName' with the actual name of your table
           'INSERT' AS Action,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing UPDATE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_update_TRAINING_SESSION
ON TRAINING_SESSION
AFTER UPDATE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'TRAINING_SESSION' AS TableName, -- Replace 'TableName' with the actual name of your table
           'UPDATE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           (SELECT * FROM inserted FOR JSON AUTO) AS New_Value,
           SUSER_SNAME() AS Changed_By;
END;

-- Trigger for auditing DELETE actions on a specific table (replace TableName with the actual name of your table)
CREATE TRIGGER trg_audit_delete_TRAINING_SESSION
ON TRAINING_SESSION
AFTER DELETE
AS
BEGIN
    INSERT INTO AUDIT_TRAIL (TableName, Action, Old_Value, New_Value, Changed_By)
    SELECT 'TRAINING_SESSION' AS TableName, -- Replace 'TableName' with the actual name of your table
           'DELETE' AS Action,
           (SELECT * FROM deleted FOR JSON AUTO) AS Old_Value,
           NULL AS New_Value, -- There's no new value for DELETE action
           SUSER_SNAME() AS Changed_By;
END;

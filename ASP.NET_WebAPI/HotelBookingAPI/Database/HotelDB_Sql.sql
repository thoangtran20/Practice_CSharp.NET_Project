CREATE DATABASE HotelDB;
GO

USE HotelDB;
GO	

-- UserRoles Table
CREATE TABLE UserRoles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50),
    IsActive BIT DEFAULT 1,
    Description NVARCHAR(255)
);
GO

-- User Information
CREATE TABLE Users (
	UserID INT PRIMARY KEY IDENTITY(1,1),
	RoleID INT,
	Email NVARCHAR(100) UNIQUE,
    PasswordHash NVARCHAR(255),
    CreatedAt DATETIME DEFAULT GETDATE(),
    LastLogin DATETIME,
    IsActive BIT DEFAULT 1,
    CreatedBy NVARCHAR(100),
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedBy NVARCHAR(100),
    ModifiedDate DATETIME,
    FOREIGN KEY (RoleID) REFERENCES UserRoles(RoleID)
);
GO	

-- Countries and States Tables
CREATE TABLE Countries (
	CountryID INT PRIMARY KEY IDENTITY(1,1),
	CountryName NVARCHAR(50),
	CountryCode NVARCHAR(10),
	IsActive BIT DEFAULT 1
);
GO	

CREATE TABLE States (
    StateID INT PRIMARY KEY IDENTITY(1,1),
    StateName NVARCHAR(50),
    CountryID INT,
    IsActive BIT DEFAULT 1,
    FOREIGN KEY (CountryID) REFERENCES Countries(CountryID)
);
GO

-- Room Types Master Data
CREATE TABLE RoomTypes (
    RoomTypeID INT PRIMARY KEY IDENTITY(1,1),
    TypeName NVARCHAR(50),
    AccessibilityFeatures NVARCHAR(255),
    Description NVARCHAR(255),
    IsActive BIT DEFAULT 1,
    CreatedBy NVARCHAR(100),
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedBy NVARCHAR(100),
    ModifiedDate DATETIME
);
GO

-- Rooms of the Hotel
CREATE TABLE Rooms (
    RoomID INT PRIMARY KEY IDENTITY(1,1),
    RoomNumber NVARCHAR(10) UNIQUE,
    RoomTypeID INT,
    Price DECIMAL(10,2),
    BedType NVARCHAR(50),
    ViewType NVARCHAR(50),
    Status NVARCHAR(50) CHECK (Status IN ('Available', 'Under Maintenance', 'Occupied')),
    IsActive BIT DEFAULT 1,
    CreatedBy NVARCHAR(100),
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedBy NVARCHAR(100),
    ModifiedDate DATETIME,
    FOREIGN KEY (RoomTypeID) REFERENCES RoomTypes(RoomTypeID)
);
GO

-- Amenities Available in the hotel
CREATE TABLE Amenities (
    AmenityID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    Description NVARCHAR(255),
    IsActive BIT DEFAULT 1,
    CreatedBy NVARCHAR(100),
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedBy NVARCHAR(100),
    ModifiedDate DATETIME
);
GO

-- Linking room types with amenities
CREATE TABLE RoomAmenities (
    RoomTypeID INT,
    AmenityID INT,
    FOREIGN KEY (RoomTypeID) REFERENCES RoomTypes(RoomTypeID),
    FOREIGN KEY (AmenityID) REFERENCES Amenities(AmenityID),
    PRIMARY KEY (RoomTypeID, AmenityID) -- Composite Primary Key to avoid duplicates
);
GO

-- The Guests who are going to stay in the hotel
CREATE TABLE Guests (
    GuestID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Email NVARCHAR(100),
    Phone NVARCHAR(15),
    AgeGroup NVARCHAR(20) CHECK (AgeGroup IN ('Adult', 'Child', 'Infant')),
    Address NVARCHAR(255),
    CountryID INT,
    StateID INT,
    CreatedBy NVARCHAR(100),
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedBy NVARCHAR(100),
    ModifiedDate DATETIME,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (CountryID) REFERENCES Countries(CountryID),
    FOREIGN KEY (StateID) REFERENCES States(StateID)
);
GO

-- Storing Reservation Information
CREATE TABLE Reservations (
    ReservationID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    RoomID INT,
    BookingDate DATE,
    CheckInDate DATE,
    CheckOutDate DATE,
    NumberOfGuests INT,
    Status NVARCHAR(50) CHECK (Status IN ('Reserved', 'Checked-in', 'Checked-out', 'Cancelled')),
    CreatedBy NVARCHAR(100),
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedBy NVARCHAR(100),
    ModifiedDate DATETIME,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (RoomID) REFERENCES Rooms(RoomID),
    CONSTRAINT CHK_CheckOutDate CHECK (CheckOutDate > CheckInDate)  
);
GO

-- Mapping table for guests linked to reservations
CREATE TABLE ReservationGuests (
    ReservationGuestID INT PRIMARY KEY IDENTITY(1,1),
    ReservationID INT,
    GuestID INT,
    FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID),
    FOREIGN KEY (GuestID) REFERENCES Guests(GuestID)
);
GO

-- Table for tracking batch payments
CREATE TABLE PaymentBatches (
    PaymentBatchID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT,
    PaymentDate DATETIME,
    TotalAmount DECIMAL(10,2),
    PaymentMethod NVARCHAR(50),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
GO

-- Individual payments Linked to Reservations and Batch Payment
CREATE TABLE Payments (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    ReservationID INT,
    Amount DECIMAL(10,2),
    PaymentBatchID INT,
    FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID),
    FOREIGN KEY (PaymentBatchID) REFERENCES PaymentBatches(PaymentBatchID)
);
GO

-- Cancellations tracking with a fee
CREATE TABLE Cancellations (
    CancellationID INT PRIMARY KEY IDENTITY(1,1),
    ReservationID INT,
    CancellationDate DATETIME,
    Reason NVARCHAR(255),
    CancellationFee DECIMAL(10,2),
    CancellationStatus NVARCHAR(50) CHECK (CancellationStatus IN ('Pending', 'Approved', 'Denied')),
    CreatedBy NVARCHAR(100),
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedBy NVARCHAR(100),
    ModifiedDate DATETIME,
    FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID)
);
GO

-- Table for Storing Refund Methods
CREATE TABLE RefundMethods (
    MethodID INT PRIMARY KEY IDENTITY(1,1),
    MethodName NVARCHAR(50),
    IsActive BIT DEFAULT 1,
);
GO

-- Table for tracking Refunds
CREATE TABLE Refunds (
    RefundID INT PRIMARY KEY IDENTITY(1,1),
    PaymentID INT,
    RefundAmount DECIMAL(10,2),
    RefundDate DATETIME DEFAULT GETDATE(),
    RefundReason NVARCHAR(255),
    RefundMethodID INT,
    ProcessedByUserID INT,
    RefundStatus NVARCHAR(50),
    FOREIGN KEY (PaymentID) REFERENCES Payments(PaymentID),
    FOREIGN KEY (RefundMethodID) REFERENCES RefundMethods(MethodID),
    FOREIGN KEY (ProcessedByUserID) REFERENCES Users(UserID)
);
GO

-- Feedback Table
CREATE TABLE Feedbacks (
    FeedbackID INT PRIMARY KEY IDENTITY(1,1),
    ReservationID INT,
    GuestID INT,
    Rating INT CHECK (Rating BETWEEN 1 AND 5),  -- Rating scale from 1 to 5
    Comment NVARCHAR(1000),  -- Optional detailed comment
    FeedbackDate DATETIME,  -- The date and time the feedback was submitted
    FOREIGN KEY (ReservationID) REFERENCES Reservations(ReservationID),
    FOREIGN KEY (GuestID) REFERENCES Guests(GuestID)
);
GO
DBCC CHECKIDENT('cities', RESEED, 0);
DBCC CHECKIDENT('clinics', RESEED, 0);
DBCC CHECKIDENT('districts', RESEED, 0);
DBCC CHECKIDENT('doctors', RESEED, 0);
DBCC CHECKIDENT('genders', RESEED, 0);
DBCC CHECKIDENT('hospitals', RESEED, 0);
DBCC CHECKIDENT('patients', RESEED, 0);
DBCC CHECKIDENT('roles', RESEED, 0);
DBCC CHECKIDENT('users', RESEED, 0);
DBCC CHECKIDENT('appointments', RESEED, 0);

DELETE FROM Hospitals
DELETE FROM Districts
DELETE FROM Cities

DELETE FROM Clinics

DELETE FROM Doctors
DELETE FROM Patients
DELETE FROM DoctorPatients

DELETE FROM Genders

DELETE FROM Roles

DELETE FROM Users

DELETE FROM Appointments

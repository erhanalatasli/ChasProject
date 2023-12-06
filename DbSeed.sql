INSERT INTO Cities (name) VALUES
('Ýstanbul'),
('Ankara'),
('Ýzmir'),
('Bursa')

INSERT INTO Districts(name, cityId) VALUES
('Bahçelievler', 1),
('Fatih', 1),
('Beþiktaþ', 1),
('Ulus', 2),
('Beypazarý', 2),
('Çankaya', 2),
('Konak', 3),
('Karþýyaka', 3),
('Buca', 3),
('Nilüfer', 4),
('Osmangazi', 4),
('Yýldýrým', 4)


INSERT INTO Hospitals(name, cityId, districtId) VALUES
('Bahçelievler Devlet Hastanesi', 1, 1),
('Fatih Sultan Mehmet Devlet Hastanesi', 1, 2),
('Beþiktaþ Sait Çiftçi Devlet Hastanesi', 1, 3),
('Ulus Devlet Hastanesi', 2, 4),
('Beypazarý Devlet Hastanesi', 2, 5),
('29 Mayýs Devlet Hastanesi', 2, 6),
('Bozyaka Eðitim ve Araþtýrma Hastanesi', 3, 7),
('Karþýyaka Devlet Hastanesi', 3, 8),
('Buca Seyfi Demirsoy Devlet Hastanesi', 3, 9),
('Bursa Þehir Hastanesi', 4, 10),
('Çekirge Devlet Hastanesi', 4, 11),
('Yüksek Ýhtisas Eðitim ve Araþtýrma Hastanesi', 4, 12)


INSERT INTO Clinics(name) VALUES
('Intensive Care'),
('Palliative Care'),
('Brain and Nerve Surgery'),
('Child Health and Diseases'),
('Infectious Diseases'),
('Physical Medicine and Rehabilitation'),
('General Surgery'),
('General Internal Medicine'),
('Cardiology'),
('Orthopedics and Traumatology'),
('Neurology'),
('Eye diseases')

INSERT INTO Genders (name) VALUES
('Erkek'),
('Kadýn')

INSERT INTO Doctors(Name, Surname, GenderId, DateOfBirth, HospitalId, ClinicId) VALUES
('Doctor1', 'Doctor111', 1, '01.01.1995', 1, 1),
('Doctor2', 'Doctor222', 2, '02.02.1994', 2, 2),
('Doctor3', 'Doctor333', 1, '03.03.1993', 3, 3),
('Doctor4', 'Doctor444', 2, '04.04.1992', 4, 4),
('Doctor5', 'Doctor555', 1, '05.05.1991', 5, 5),
('Doctor6', 'Doctor666', 2, '06.06.1990', 6, 6),
('Doctor7', 'Doctor777', 1, '07.07.1989', 7, 7),
('Doctor8', 'Doctor888', 2, '08.08.1988', 8, 8),
('Doctor9', 'Doctor999', 1, '09.09.1987', 9, 9),
('Doctor10', 'Doctor100', 2, '10.10.1986', 10, 10),
('Doctor11', 'Doctor1111', 1, '11.11.1985', 11, 11),
('Doctor12', 'Doctor1222', 2, '12.12.1984', 12, 12)


INSERT INTO Patients(Name, Surname, GenderId, DateOfBirth, CityId, Complaint, IsIssurance) VALUES
('Patient1', 'Patient111', 1, '01.01.1995', 1, 'Complaint1', 1),
('Patient2', 'Patient222', 2, '02.02.1994', 2, 'Complaint2', 1),
('Patient3', 'Patient333', 1, '03.03.1993', 3, 'Complaint3', 1),
('Patient4', 'Patient444', 2, '04.04.1992', 4, 'Complaint4', 1),
('Patient5', 'Patient555', 1, '05.05.1991', 1, 'Complaint5', 1),
('Patient6', 'Patient666', 2, '06.06.1990', 2, 'Complaint6', 0),
('Patient7', 'Patient777', 1, '07.07.1989', 3, 'Complaint7', 1),
('Patient8', 'Patient888', 2, '08.08.1988', 4, 'Complaint8', 1),
('Patient9', 'Patient999', 1, '09.09.1987', 1, 'Complaint9', 1),
('Patient10', 'Patient100', 2, '10.10.1986', 2, 'Complaint10', 0),
('Patient11', 'Patient1111', 1, '11.11.1985', 3, 'Complaint11', 1),
('Patient12', 'Patient1222', 2, '12.12.1984', 4, 'Complaint12', 1)



INSERT INTO Roles (name) VALUES
('Admin'),
('Doctor'),
('Patient'),
('Nurse')

INSERT INTO Users (Name, Surname, UserName, Password, IsActive, RoleId) VALUES
('Admin', 'Admin', 'Admin', 'Admin', 1, 1),
('Doctor1', 'Doctor111', 'Doctor1', 'Doctor111', 1, 2),
('Nurse1', 'Nurse111', 'Nurse1', 'Nurse111', 1, 4),
('User1', 'User111', 'User1', 'User111', 1, 3),
('User2', 'User222', 'User2', 'User222', 1, 3),
('User3', 'User333', 'User3', 'User333', 1, 3),
('User4', 'User444', 'User4', 'User444', 1, 3)

INSERT INTO Appointments(UserId, HospitalId, ClinicId, DoctorId, Date, IsActive, Explanation) VALUES
(4, 1, 1, 1, '02.02.2023', 1, 'Explanation1'),
(5, 2, 2, 2, '02.08.2023', 1, 'Explanation2'),
(6, 3, 3, 3, '12.05.2023', 1, 'Explanation3'),
(7, 4, 4, 4, '09.12.2023', 1, 'Explanation4')



INSERT INTO DoctorPatients(DoctorId, PatientId) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10),
(11, 11),
(12, 12)

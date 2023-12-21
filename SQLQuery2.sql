INSERT INTO AspNetUsers (Id, Aktiv, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount)
VALUES 
('user-id-1', 1, 0, 0, 0, 0, 0),
('user-id-2', 1, 0, 0, 0, 0, 0),
('user-id-3', 1, 0, 0, 0, 0, 0);



INSERT INTO Projekt (Titel, Beskrivning, AnvändarId)
VALUES 
('Projekt 1', 'Beskrivning av Projekt 1', 'user-id-1'),
('Projekt 2', 'Beskrivning av Projekt 2', 'user-id-2'),
('Projekt 3', 'Beskrivning av Projekt 3', 'user-id-3');






INSERT INTO AspNetUsers (Id, Aktiv, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount)
VALUES 
('user-id-1', 1, 0, 0, 0, 0, 0),
('user-id-2', 1, 0, 0, 0, 0, 0),
('user-id-3', 1, 0, 0, 0, 0, 0);

INSERT INTO Projekt (Titel, Beskrivning, AnvändarId)
VALUES 
('Projekt 1', 'Podd-projektet', 'user-id-1'),
('Projekt 2', 'Webbdesign-projektet', 'user-id-2'),
('Projekt 3', 'UX-design-projektet', 'user-id-3');

DELETE FROM Projekt WHERE Titel IN ('Projekt 1', 'Projekt 2', 'Projekt 3');


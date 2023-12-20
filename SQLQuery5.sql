DELETE FROM Projekt WHERE Titel IN ('Projekt 1', 'Projekt 2', 'Projekt 3');


DELETE FROM AspNetUsers WHERE Id IN ('user-id-1', 'user-id-2', 'user-id-3');


INSERT INTO AspNetUsers (Id, Aktiv, EmailConfirmed, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount)
VALUES 
('user-id-1', 1, 0, 0, 0, 0, 0),
('user-id-2', 1, 0, 0, 0, 0, 0),
('user-id-3', 1, 0, 0, 0, 0, 0);

INSERT INTO Projekt (Titel, Beskrivning, AnvändarId)
VALUES 
('UX-design-projektet', 'Designade en innovativ applikation', 'user-id-1'),
('Webbdesign-projektet', 'Byggde en hemsida med HTML/CSS/JS', 'user-id-2'),
('Podd-projektet', 'Skapade en podcast-spelare i C#', 'user-id-3');


ALTER TABLE Projekt ADD SkapadDatum DATETIME NOT NULL DEFAULT GETDATE();



﻿-- user1: Andrzej Duda11 65001122410000000000000001
-- user2: Admin Admin 38001122410000000000000002

GO
INSERT INTO [User] ( Name, Password)
VALUES ('Andrzej', 'fBazT746AC35JQS76GaPBTVyeo/i6e/rJTYF6w2VtkPL5tmO');

GO
INSERT INTO [Account](UserId, Balance, Number)
VALUES (1, 1000, '65001122410000000000000001');

GO
INSERT INTO [User] (Name, Password)
VALUES ('Admin', '9jPXRTsxKEIPdrtK7PtNw0vOYbsxLGevcq6C5O2bRFelTgJI');

GO
INSERT INTO [Account](UserId, Balance, Number)
VALUES (2, 400, '38001122410000000000000002');

GO
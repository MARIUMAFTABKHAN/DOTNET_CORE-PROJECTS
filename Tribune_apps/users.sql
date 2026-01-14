use JTS


--select * from tblUser 

--select * from tblUserChannel where ChannelId=7


select uc.UserChannelId,uc.ChannelId,c.Name,u.UserId,u.FullName,u.UserName,u.Password,u.IsActive
from tblUserChannel uc
inner join tblUser u on uc.UserId=u.UserId
inner join tblChannel c on uc.ChannelId=c.ChannelId
where u.IsActive=1 and c.ChannelId=7
order by u.UserId asc


--select * from tblChannel

SELECT DISTINCT u.*
FROM tblUser u
JOIN tblUserChannel uc ON u.UserId = uc.UserId
JOIN tblChannel c ON uc.ChannelId = c.ChannelId
WHERE u.IsActive = 1
  AND c.ChannelId = 7;




  USE EXPARCS;
GO

CREATE TABLE dbo.tblUser_JTSImported (
    UserId               INT            NOT NULL,
    FullName             VARCHAR(30)    NOT NULL,
    UserName             VARCHAR(30)    NOT NULL,
    [Password]           VARCHAR(15)    NOT NULL,
    Email                VARCHAR(50)    NOT NULL,
    CityId               INT            NOT NULL,
    RoleId               INT            NOT NULL,
    UserPriority         BIT            NOT NULL,
    OffDay               VARCHAR(10)    NULL,
    IsActive             BIT            NOT NULL,
    CreatedByUserId      INT            NULL,
    CreatedOn            DATETIME       NULL,
    LastModifiedByUserId INT            NULL,
    LastModifiedOn       DATETIME       NULL
);


INSERT INTO EXPARCS.dbo.tblUser_JTSImported (
    UserId, FullName, UserName, [Password], Email, CityId, RoleId,
    UserPriority, OffDay, IsActive, CreatedByUserId, CreatedOn,
    LastModifiedByUserId, LastModifiedOn
)
SELECT DISTINCT
    u.UserId, u.FullName, u.UserName, u.[Password], u.Email, u.CityId, u.RoleId,
    u.UserPriority, u.OffDay, u.IsActive, u.CreatedByUserId, u.CreatedOn,
    u.LastModifiedByUserId, u.LastModifiedOn
FROM JTS.dbo.tblUser u
JOIN JTS.dbo.tblUserChannel uc ON u.UserId = uc.UserId
JOIN JTS.dbo.tblChannel c ON uc.ChannelId = c.ChannelId
WHERE u.IsActive = 1
  AND c.ChannelId = 7;


  select * from tblUser_JTSImported
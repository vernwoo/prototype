use Authz

if not exists(select 1 from PERMISSION where name = 'ReadOperation')
	BEGIN
		INSERT INTO PERMISSION VALUES('ReadOperation', 'Read data from a tool');
		PRINT 'ReadOperation permission added';
	END

if not exists(select 1 from PERMISSION where name = 'WriteOperation')
	BEGIN
		INSERT INTO PERMISSION VALUES('WriteOperation', 'Write data to a tool');
		PRINT 'WriteOperation permission added';
	END
	
if not exists(select 1 from ROLE where name = 'ReadRole')
	BEGIN
		INSERT INTO ROLE VALUES('ReadRole', 'Read role for all read operations');
		PRINT 'ReadRole added';
	END

if not exists(select 1 from ROLE where name = 'WriteRole')
	BEGIN
		INSERT INTO ROLE VALUES('WriteRole', 'Write role for all write operations');
		PRINT 'WriteRole added';
	END

--ReadRole map to all read permissions
DECLARE @roleId int;
DECLARE @permId int;
SELECT @roleId = id FROM ROLE where name ='ReadRole';
SELECT @permId = id FROM PERMISSION where name = 'ReadOperation';
INSERT INTO ROlE_PERMISSION VALUES (@roleId, @permId) 

--WritRole map to all write permissions
SELECT @roleId = id FROM ROLE where name ='WriteRole';
SELECT @permId = id FROM PERMISSION where name = 'WriteOperation';
INSERT INTO ROLE_PERMISSION VALUES(@roleId, @permId);

--vern is assigned a WriteRole
INSERT INTO ROLE_ASSIGNMENT VALUES(@roleId, 'vernwoo@gmail.com');

GO

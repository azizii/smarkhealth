﻿select * from children

select * from children c
join guardians g on g.guardianid=c.guardianid
join messes m on m.Messid=g.messid
where m.messid=10


select name from (select worker.name, tbl1.worker_id from worker right join (select worker_id from money where MONTH(date) = 12) as tb1 on worker.id = tbl1.worker_id) as tbl2 where worker_id is null
use azure_db;

show tables ;

#テーブルの作成
create table  2d_action
(player_id int,
 stage int,
 time float,
 created_at datetime default current_timestamp,
 update_at datetime default current_timestamp on update current_timestamp,
 primary key(player_id,stage)
);

#プレイヤー情報をインサート
insert  into 2d_action(player_id, stage,time)  VALUES (@UserId,@Stage,@time)
on duplicate key update time = if(time<values(time),time,values(time) );

#プレイヤー情報の全表示
select * from 2d_action;

#ステージごとのランキング表示
select player_id,time from 2d_action  where stage = @stage order by time limit 5;

#ユーザ数の取得
select count(distinct player_id) as member from 2d_action;

delete from 2d_action where time = 0;

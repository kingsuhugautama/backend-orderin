--
-- PostgreSQL database dump
--

-- Dumped from database version 10.16
-- Dumped by pg_dump version 10.16

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


--
-- Name: bannermenu_deletedata(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.bannermenu_deletedata(p_bannermenuid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int; 

BEGIN

DELETE FROM bannermenu 
 WHERE bannermenuid=p_bannermenuid;

 GET DIAGNOSTICS rcount = ROW_COUNT; 

if(rcount > 0) then
    return query select 1;
else
    return query select 0;
end if;

    
END;
$$;


ALTER FUNCTION public.bannermenu_deletedata(p_bannermenuid integer) OWNER TO postgres;

--
-- Name: bannermenu_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.bannermenu_getalldata() RETURNS TABLE(bannermenuid integer, bannermenuname character varying, bannerimageurl character varying, cityid integer, cityname character varying)
    LANGUAGE plpgsql
    AS $$

BEGIN

return query 
 SELECT 
b.bannermenuid,
b.bannermenuname,
b.bannerimageurl,
b.cityid,
mk.nama cityname
FROM bannermenu b
left join masterkota mk on b.cityid = mk.id;

END;
$$;


ALTER FUNCTION public.bannermenu_getalldata() OWNER TO postgres;

--
-- Name: bannermenu_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.bannermenu_getdatabydynamicfilters(filters character varying) RETURNS TABLE(bannermenuid integer, bannermenuname character varying, bannerimageurl character varying, cityid integer, cityname character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from bannermenu_GetAllData();
ELSE
    sql = 'SELECT 
b.bannermenuid,
b.bannermenuname,
b.bannerimageurl,
b.cityid,
mk.nama cityname
FROM bannermenu b
left join masterkota mk on b.cityid = mk.id
 WHERE ' || filters || '
 Order By bannermenuid';
 
 return query EXECUTE sql; 
 
 END IF;

END;
$$;


ALTER FUNCTION public.bannermenu_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: bannermenu_insertdata(character varying, character varying, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.bannermenu_insertdata(p_bannermenuname character varying, p_bannerimageurl character varying, p_cityid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int; 

BEGIN

INSERT INTO bannermenu 
(
	bannermenuname,
	bannerimageurl,
	cityid
) 
 
 VALUES(
	p_bannermenuname,
	p_bannerimageurl,
	 p_cityid
);

 GET DIAGNOSTICS rcount = ROW_COUNT; 

if(rcount > 0) then
    return query select 1;
else
    return query select 0;
end if;

    
END;
$$;


ALTER FUNCTION public.bannermenu_insertdata(p_bannermenuname character varying, p_bannerimageurl character varying, p_cityid integer) OWNER TO postgres;

--
-- Name: bannermenu_updatedata(integer, character varying, character varying, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.bannermenu_updatedata(p_bannermenuid integer, p_bannermenuname character varying, p_bannerimageurl character varying, p_cityid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int; 

BEGIN

UPDATE bannermenu set 
bannermenuname=p_bannermenuname,
bannerimageurl=p_bannerimageurl,
cityid=p_cityid
 
 WHERE bannermenuid=p_bannermenuid;

 GET DIAGNOSTICS rcount = ROW_COUNT; 
 
if(rcount > 0) then
    return query select 1;
else
    return query select 0;
end if;

    
END;
$$;


ALTER FUNCTION public.bannermenu_updatedata(p_bannermenuid integer, p_bannermenuname character varying, p_bannerimageurl character varying, p_cityid integer) OWNER TO postgres;

--
-- Name: favoritproduct_deletedata(integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.favoritproduct_deletedata(p_productid integer, p_userid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

DELETE FROM favoritproduct 
 
 WHERE productid=p_productid AND 
userid=p_userid 
RETURNING productid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.favoritproduct_deletedata(p_productid integer, p_userid integer) OWNER TO postgres;

--
-- Name: favoritproduct_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.favoritproduct_getalldata() RETURNS TABLE(productid integer, userid integer, waktuentry timestamp without time zone)
    LANGUAGE plpgsql
    AS $$

BEGIN

return query 
 SELECT 
f.productid,
f.userid,
f.waktuentry
FROM favoritproduct f;


END;
$$;


ALTER FUNCTION public.favoritproduct_getalldata() OWNER TO postgres;

--
-- Name: favoritproduct_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.favoritproduct_getdatabydynamicfilters(filters character varying) RETURNS TABLE(productid integer, userid integer, waktuentry timestamp without time zone)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from favoritproduct_GetAllData();
ELSE
    sql = 'SELECT 
f.productid,
f.userid,
f.waktuentry
FROM favoritproduct f
 WHERE ' || filters || '
 Order By productid';
 
 return query EXECUTE sql; 
 
 END IF;


END;
$$;


ALTER FUNCTION public.favoritproduct_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: favoritproduct_insertdata(integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.favoritproduct_insertdata(p_productid integer, p_userid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

INSERT INTO favoritproduct 
(
productid,
userid,
waktuentry
) 
 
 VALUES(
p_productid,
p_userid,
	 current_timestamp
) RETURNING productid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.favoritproduct_insertdata(p_productid integer, p_userid integer) OWNER TO postgres;

--
-- Name: getweekfromdate(timestamp without time zone); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.getweekfromdate(p_date timestamp without time zone) RETURNS integer
    LANGUAGE plpgsql
    AS $$
begin 
	return extract('day' from date_trunc('week', p_date) -
                   date_trunc('week', date_trunc('month', p_date))) / 7 + 1;
end;
$$;


ALTER FUNCTION public.getweekfromdate(p_date timestamp without time zone) OWNER TO postgres;

--
-- Name: grafik_getalldataanalisacustomerpergender(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getalldataanalisacustomerpergender() RETURNS TABLE(merchantid integer, merchantname character varying, gender text, jumlah bigint)
    LANGUAGE plpgsql
    AS $$
BEGIN

return query 
select mm.merchantid,mm.merchantname,
case 
when u.gender = 'l' then 'Pria'
when u.gender = 'p' then 'Wanita'
else '-' 
end gender, count(u.gender)  jumlah
from transorderheader t 
inner join users u on t.userentry = u.userid
inner join transopenpoheader toh on t.openpoheaderid = toh.openpoheaderid 
inner join mastermerchant mm on toh.merchantid = mm.merchantid 

group by  mm.merchantid,mm.merchantname,u.gender;

END;
$$;


ALTER FUNCTION public.grafik_getalldataanalisacustomerpergender() OWNER TO postgres;

--
-- Name: grafik_getalldataanalisacustomerpergenderbydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getalldataanalisacustomerpergenderbydynamicfilters(filters character varying) RETURNS TABLE(merchantid integer, merchantname character varying, gender text, jumlah bigint)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from grafik_getalldataanalisacustomerpergender();
ELSE
    sql = 'select mm.merchantid,mm.merchantname,
case 
when u.gender = ''l'' then ''Pria''
when u.gender = ''p'' then ''Wanita''
else ''-''
end gender, count(u.gender)  jumlah
from transorderheader t 
inner join users u on t.userentry = u.userid
inner join transopenpoheader toh on t.openpoheaderid = toh.openpoheaderid 
inner join mastermerchant mm on toh.merchantid = mm.merchantid 
 WHERE ' || filters || '
group by  mm.merchantid,mm.merchantname,u.gender
 Order By mm.merchantname';
 
 return query EXECUTE sql; 
 
 END IF;



    
END;
$$;


ALTER FUNCTION public.grafik_getalldataanalisacustomerpergenderbydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: grafik_getalldataanalisacustomerperusia(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getalldataanalisacustomerperusia() RETURNS TABLE(merchantid integer, merchantname character varying, usia text, jumlah bigint)
    LANGUAGE plpgsql
    AS $$
BEGIN

return query 
select a.merchantid,a.merchantname,a.usia,count(a.jumlah) jumlah 
from(
select mm.merchantid,mm.merchantname,
case
when extract(year from age(current_date,u.birthdate)) between 0 and 10 then '1 - 10'
when extract(year from age(current_date,u.birthdate)) between 11 and 20 then '11 - 20'
when extract(year from age(current_date,u.birthdate)) between 21 and 30 then '21 - 30'
when extract(year from age(current_date,u.birthdate)) between 31 and 40 then '31 - 40'
when extract(year from age(current_date,u.birthdate)) between 41 and 50 then '41 - 50'
when extract(year from age(current_date,u.birthdate)) between 51 and 60 then '51 - 60'
when extract(year from age(current_date,u.birthdate)) between 61 and 70 then '61 - 70'
when extract(year from age(current_date,u.birthdate)) between 71 and 80 then '71 - 80'
when extract(year from age(current_date,u.birthdate)) between 81 and 90 then '81 - 90'
--else '< 1' 
end usia,
case
when extract(year from age(current_date,u.birthdate)) between 20 and 30 then 1
else 0 
end jumlah
from transorderheader t 
inner join users u on t.userentry = u.userid
inner join transopenpoheader toh on t.openpoheaderid = toh.openpoheaderid 
inner join mastermerchant mm on toh.merchantid = mm.merchantid
) a
group by a.merchantid,a.merchantname,a.usia;

END;
$$;


ALTER FUNCTION public.grafik_getalldataanalisacustomerperusia() OWNER TO postgres;

--
-- Name: grafik_getalldataanalisacustomerperusiabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getalldataanalisacustomerperusiabydynamicfilters(filters character varying) RETURNS TABLE(merchantid integer, merchantname character varying, usia text, jumlah bigint)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from grafik_getalldataanalisacustomerperusia();
ELSE
    sql = 'select merchantid,merchantname,usia,count(jumlah) jumlah 
from(
select mm.merchantid,mm.merchantname,
case
when extract(year from age(current_date,u.birthdate)) between 0 and 10 then ''1 - 10''
when extract(year from age(current_date,u.birthdate)) between 11 and 20 then ''11 - 20''
when extract(year from age(current_date,u.birthdate)) between 21 and 30 then ''21 - 30''
when extract(year from age(current_date,u.birthdate)) between 31 and 40 then ''31 - 40''
when extract(year from age(current_date,u.birthdate)) between 41 and 50 then ''41 - 50''
when extract(year from age(current_date,u.birthdate)) between 51 and 60 then ''51 - 60''
when extract(year from age(current_date,u.birthdate)) between 61 and 70 then ''61 - 70''
when extract(year from age(current_date,u.birthdate)) between 71 and 80 then ''71 - 80''
when extract(year from age(current_date,u.birthdate)) between 81 and 90 then ''81 - 90''
--else ''< 1'' 
end usia,
case
when extract(year from age(current_date,u.birthdate)) between 20 and 30 then 1
else 0 
end jumlah
from transorderheader t 
inner join users u on t.userentry = u.userid
inner join transopenpoheader toh on t.openpoheaderid = toh.openpoheaderid 
inner join mastermerchant mm on toh.merchantid = mm.merchantid
 WHERE ' || filters || '
) a
group by merchantid,merchantname,usia
 Order By merchantid;';
 
 return query EXECUTE sql; 
 
 END IF;

    
END;
$$;


ALTER FUNCTION public.grafik_getalldataanalisacustomerperusiabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: grafik_getalldataanalisacustomerrepeatorder(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getalldataanalisacustomerrepeatorder() RETURNS TABLE(merchantid integer, merchantname character varying, productid integer, productname character varying, userbaru bigint, userlama bigint)
    LANGUAGE plpgsql
    AS $$
BEGIN

return query 

select 
a.merchantid,a.merchantname,a.productid,a.productname,
sum(
case when a.jenis = 'baru' then 1 else 0 end
) userbaru,
sum(
case when a.jenis = 'lama' then 1 else 0 end
) userlama
from (

select distinct
mm.merchantid,mm.merchantname,
mp.productid,
mp.productname,
u.userid,
case
when count(t.orderheaderid) over(partition by mp.productid,u.userid) > 1 then 'lama'
else 'baru'
end jenis
from 
transorderheader t 
inner join transorderdetail tod on t.orderheaderid = tod.orderheaderid 
inner join transopenpodetailproduct topdp on tod.openpodetailproductid = topdp.openpodetailproductid
inner join masterproduct mp on topdp.productid = mp.productid --and mp.isactive = true
inner join mastermerchant mm on mp.merchantid = mm.merchantid 
inner join users u on t.userentry = u.userid 
group by mm.merchantid,mm.merchantname,
mp.productid,mp.productname,u.userid,t.orderheaderid
order by mp.productid 

 )a
group by a.merchantid ,a.merchantname,a.productid,a.productname;

    
END;
$$;


ALTER FUNCTION public.grafik_getalldataanalisacustomerrepeatorder() OWNER TO postgres;

--
-- Name: grafik_getalldataomsetpercategory(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getalldataomsetpercategory() RETURNS TABLE(merchantid integer, merchantname character varying, categorymenuid integer, categorymenuname character varying, qty numeric, total numeric)
    LANGUAGE plpgsql
    AS $$
begin
return query SELECT mm.merchantid,mm.merchantname,mcm.categorymenuid,mcm.categorymenuname,
sum(tod.qty) qty,sum(tod.subtotal) total
FROM 
transorderheader t 
inner join transorderdetail tod on t.orderheaderid = tod.orderheaderid 
inner join transopenpodetailproduct topdp on tod.openpodetailproductid = topdp.openpodetailproductid 
inner join masterproduct mp on topdp.productid = mp.productid --and mp.isactive = true 
inner join mastercategorymenu mcm on mp.categorymenuid = mcm.categorymenuid 
inner join mastermerchant mm on mp.merchantid  = mm.merchantid
group by mm.merchantid,mcm.categorymenuid;

end;
$$;


ALTER FUNCTION public.grafik_getalldataomsetpercategory() OWNER TO postgres;

--
-- Name: grafik_getalldataomsetperdropship(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getalldataomsetperdropship() RETURNS TABLE(merchantid integer, merchantname character varying, dropshipid integer, label character varying, qty numeric, total numeric)
    LANGUAGE plpgsql
    AS $$
begin
return query SELECT mm.merchantid,mm.merchantname,md.dropshipid,
md.label ,sum(tod.qty) qty,sum(tod.subtotal) total
FROM 
transorderdetail tod
inner join transopenpodetailproduct topdp on tod.openpodetailproductid = topdp.openpodetailproductid
inner join masterproduct mp on topdp.productid = mp.productid --and mp.isactive = true
inner join transopenpodetaildropship topdd on topdp.openpodetaildropshipid = topdd.openpodetaildropshipid
inner join masterdropship md on topdd.dropshipid = md.dropshipid 
inner join mastermerchant mm on md.merchantid  = mm.merchantid 
group by mm.merchantid,md.dropshipid;

end;
$$;


ALTER FUNCTION public.grafik_getalldataomsetperdropship() OWNER TO postgres;

--
-- Name: grafik_getalldataomsetperdropshipproduct(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getalldataomsetperdropshipproduct() RETURNS TABLE(merchantid integer, merchantname character varying, dropshipid integer, label character varying, productid integer, productname character varying, qty numeric, total numeric)
    LANGUAGE plpgsql
    AS $$
begin
return query SELECT mm.merchantid,mm.merchantname,md.dropshipid,md.label,mp.productid,mp.productname,
sum(tod.qty) qty,sum(tod.subtotal) total
FROM transorderdetail tod  
inner join transopenpodetailproduct topdp on tod.openpodetailproductid = topdp.openpodetailproductid
inner join transopenpodetaildropship topdd on topdp.openpodetaildropshipid = topdd.openpodetaildropshipid
inner join masterproduct mp on topdp.productid = mp.productid --and mp.isactive = true
inner join masterdropship md on topdd.dropshipid = md.dropshipid 
inner join mastermerchant mm on md.merchantid  = mm.merchantid 
group by mm.merchantid,md.dropshipid,mp.productid;

end;
$$;


ALTER FUNCTION public.grafik_getalldataomsetperdropshipproduct() OWNER TO postgres;

--
-- Name: grafik_getalldataomsetperproduct(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getalldataomsetperproduct() RETURNS TABLE(merchantid integer, merchantname character varying, productid integer, productname character varying, qty numeric, total numeric)
    LANGUAGE plpgsql
    AS $$
begin
return query SELECT mm.merchantid,mm.merchantname ,mp.productid,mp.productname ,coalesce(sum(tod.qty),0) qty,
 coalesce(sum(tod.subtotal),0) total
FROM transorderdetail tod 
inner join transopenpodetailproduct topdp on tod.openpodetailproductid  = topdp .openpodetailproductid 
right join masterproduct mp on topdp.productid = mp.productid
inner join mastermerchant mm on mp.merchantid  = mm.merchantid 

group by mm.merchantid,mp.productid
having coalesce(sum(tod.qty),0) > 0
order by mp.productname;

end;
$$;


ALTER FUNCTION public.grafik_getalldataomsetperproduct() OWNER TO postgres;

--
-- Name: grafik_getalldataongkoskirimperdropship(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getalldataongkoskirimperdropship() RETURNS TABLE(merchantid integer, merchantname character varying, dropshipid integer, label character varying, totalongkoskirim numeric)
    LANGUAGE plpgsql
    AS $$
BEGIN

return query 
select 
mm.merchantid,mm.merchantname,md.dropshipid,md.label, sum(topdd.ongkoskirim) totalongkoskirim 
from transorderheader t
inner join transopenpodetaildropship topdd on t.openpodetaildropshipid = topdd.openpodetaildropshipid
inner join masterdropship md on topdd.dropshipid = md.dropshipid 
inner join mastermerchant mm on md.merchantid = mm.merchantid

group by mm.merchantid,md.dropshipid; 

END;
$$;


ALTER FUNCTION public.grafik_getalldataongkoskirimperdropship() OWNER TO postgres;

--
-- Name: grafik_getalldatapengirimanperdropship(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getalldatapengirimanperdropship() RETURNS TABLE(merchantid integer, merchantname character varying, dropshipid integer, label character varying, shipmentid integer, keterangan character varying, jumlah bigint)
    LANGUAGE plpgsql
    AS $$

BEGIN

return query 
select  mm.merchantid,mm.merchantname,md.dropshipid,md."label",ms.shipmentid ,ms.keterangan,count(ms.shipmentid) jumlah
from transpengiriman t
inner join transorderheader toh on t.orderheaderid = toh.orderheaderid
inner join transopenpodetaildropship topdd on toh.openpodetaildropshipid = topdd.openpodetaildropshipid 
inner join masterdropship md on topdd.dropshipid = md.dropshipid 
inner join mastermerchant mm on md.merchantid = mm.merchantid 
inner join mastershipment ms on t.shipmentid = ms.shipmentid 
group by mm.merchantid,mm.merchantname,md.dropshipid,md."label",ms.shipmentid,ms.keterangan;

    
END;
$$;


ALTER FUNCTION public.grafik_getalldatapengirimanperdropship() OWNER TO postgres;

--
-- Name: grafik_getallomsetpertanggalpo(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getallomsetpertanggalpo() RETURNS TABLE(merchantid integer, merchantname character varying, tglpo character varying, total numeric)
    LANGUAGE plpgsql
    AS $$

begin
return query 
select mm.merchantid,mm.merchantname, date(toph.openpodate)::varchar tglpo,sum(t.total) total 
from transorderheader t
inner join transopenpoheader toph on t.openpoheaderid = toph.openpoheaderid
inner join mastermerchant mm on toph.merchantid = mm.merchantid 
group by mm.merchantid,date(toph.openpodate)
order by date(toph.openpodate);

end;
$$;


ALTER FUNCTION public.grafik_getallomsetpertanggalpo() OWNER TO postgres;

--
-- Name: grafik_getdataanalisacustomerrepeatorderbydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getdataanalisacustomerrepeatorderbydynamicfilters(filters character varying) RETURNS TABLE(merchantid integer, merchantname character varying, productid integer, productname character varying, userbaru bigint, userlama bigint)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
    begin
	    if filters='' then 
    return query select * from grafik_getalldataanalisacustomerrepeatorder();
ELSE
    sql = 'select 
a.merchantid,a.merchantname,a.productid,a.productname,
sum(
case when a.jenis = ''baru'' then 1 else 0 end
) userbaru,
sum(
case when a.jenis = ''lama'' then 1 else 0 end
) userlama
from (

select distinct
mm.merchantid,mm.merchantname,
mp.productid,
mp.productname,
u.userid,
case
when count(t.orderheaderid) over(partition by mp.productid,u.userid) > 1 then ''lama''
else ''baru''
end jenis
from 
transorderheader t 
inner join transorderdetail tod on t.orderheaderid = tod.orderheaderid 
inner join transopenpodetailproduct topdp on tod.openpodetailproductid = topdp.openpodetailproductid
inner join masterproduct mp on topdp.productid = mp.productid --and mp.isactive = true
inner join mastermerchant mm on mp.merchantid = mm.merchantid 
inner join users u on t.userentry = u.userid 
 WHERE ' || filters || ' 
group by mm.merchantid,mm.merchantname,
mp.productid,mp.productname,u.userid,t.orderheaderid
 )a
group by a.merchantid ,a.merchantname,a.productid,a.productname
 Order By a.productid';
 
 return query EXECUTE sql; 
 
 END IF;
    end;
$$;


ALTER FUNCTION public.grafik_getdataanalisacustomerrepeatorderbydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: grafik_getdataomsetpercategorybydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getdataomsetpercategorybydynamicfilters(filters character varying) RETURNS TABLE(merchantid integer, merchantname character varying, categorymenuid integer, categorymenuname character varying, qty numeric, total numeric)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from grafik_getalldataomsetpercategory();
ELSE
	sql = 'SELECT mm.merchantid,mm.merchantname,mcm.categorymenuid,mcm.categorymenuname,
sum(tod.qty) qty,sum(tod.subtotal) total
FROM 
transorderheader t 
inner join transorderdetail tod on t.orderheaderid = tod.orderheaderid 
inner join transopenpodetailproduct topdp on tod.openpodetailproductid = topdp.openpodetailproductid 
inner join masterproduct mp on topdp.productid = mp.productid --and mp.isactive = true 
inner join mastercategorymenu mcm on mp.categorymenuid = mcm.categorymenuid 
inner join mastermerchant mm on mp.merchantid  = mm.merchantid 
where ' || filters || '
group by mm.merchantid,mcm.categorymenuid
order by mcm.categorymenuid';

 return query EXECUTE sql; 
end if;
end;
$$;


ALTER FUNCTION public.grafik_getdataomsetpercategorybydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: grafik_getdataomsetperdropshipbydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getdataomsetperdropshipbydynamicfilters(filters character varying) RETURNS TABLE(merchantid integer, merchantname character varying, dropshipid integer, label character varying, qty numeric, total numeric)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from grafik_getalldataomsetperdropship();
ELSE
	sql = 'SELECT mm.merchantid,mm.merchantname,md.dropshipid,
md.label ,sum(tod.qty) qty,sum(tod.subtotal) total
FROM transorderheader t
inner join transopenpoheader toph on t.openpoheaderid = toph.openpoheaderid
inner join transorderdetail tod on t.orderheaderid = tod.orderheaderid
inner join transopenpodetailproduct topdp on tod.openpodetailproductid = topdp.openpodetailproductid
inner join masterproduct mp on topdp.productid = mp.productid --and mp.isactive = true
inner join transopenpodetaildropship topdd on topdp.openpodetaildropshipid = topdd.openpodetaildropshipid
inner join masterdropship md on topdd.dropshipid = md.dropshipid 
inner join mastermerchant mm on md.merchantid  = mm.merchantid 
where ' || filters || '
group by mm.merchantid,md.dropshipid
order by md.dropshipid';

 return query EXECUTE sql; 
end if;
end;
$$;


ALTER FUNCTION public.grafik_getdataomsetperdropshipbydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: grafik_getdataomsetperdropshipproductbydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getdataomsetperdropshipproductbydynamicfilters(filters character varying) RETURNS TABLE(merchantid integer, merchantname character varying, dropshipid integer, label character varying, productid integer, productname character varying, qty numeric, total numeric)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from grafik_getalldataomsetperdropshipproduct();
ELSE
	sql = 'SELECT mm.merchantid,mm.merchantname,md.dropshipid,md.label,mp.productid,mp.productname,
sum(tod.qty) qty,sum(tod.subtotal) total
FROM transorderheader t
inner join transorderdetail tod on t.orderheaderid = tod.orderheaderid
inner join transopenpodetailproduct topdp on tod.openpodetailproductid = topdp.openpodetailproductid
inner join transopenpodetaildropship topdd on topdp.openpodetaildropshipid = topdd.openpodetaildropshipid
inner join masterproduct mp on topdp.productid = mp.productid --and mp.isactive = true
inner join masterdropship md on topdd.dropshipid = md.dropshipid 
inner join mastermerchant mm on md.merchantid  = mm.merchantid 

where ' || filters || '
group by mm.merchantid,md.dropshipid,mp.productid
order by md.dropshipid';

 return query EXECUTE sql; 
end if;
end;
$$;


ALTER FUNCTION public.grafik_getdataomsetperdropshipproductbydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: grafik_getdataomsetperproductbydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getdataomsetperproductbydynamicfilters(filters character varying) RETURNS TABLE(merchantid integer, merchantname character varying, productid integer, productname character varying, qty numeric, total numeric)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from grafik_getalldataomsetperproduct();
ELSE
	sql = ' SELECT mm.merchantid,mm.merchantname ,mp.productid,mp.productname ,coalesce(sum(tod.qty),0) qty,
 coalesce(sum(tod.subtotal),0) total
FROM transorderheader t
inner join transorderdetail tod on t.orderheaderid = tod.orderheaderid 
inner join transopenpodetailproduct topdp on tod.openpodetailproductid  = topdp .openpodetailproductid 
right join masterproduct mp on topdp.productid = mp.productid
inner join mastermerchant mm on mp.merchantid  = mm.merchantid 
where ' || filters || '
group by mp.productid,mm.merchantid
having coalesce(sum(tod.qty),0) > 0
order by mp.productid';

 return query EXECUTE sql; 
end if;
end;
$$;


ALTER FUNCTION public.grafik_getdataomsetperproductbydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: grafik_getdatapengirimanperdropshipbydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getdatapengirimanperdropshipbydynamicfilters(filters character varying) RETURNS TABLE(merchantid integer, merchantname character varying, dropshipid integer, label character varying, shipmentid integer, keterangan character varying, jumlah bigint)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from grafik_GetAllDatapengirimanperdropship();
ELSE
    sql = 'select  mm.merchantid,mm.merchantname,md.dropshipid,md."label",ms.shipmentid ,ms.keterangan,count(ms.shipmentid) jumlah
from transpengiriman t
inner join transorderheader toh on t.orderheaderid = toh.orderheaderid
inner join transopenpodetaildropship topdd on toh.openpodetaildropshipid = topdd.openpodetaildropshipid 
inner join masterdropship md on topdd.dropshipid = md.dropshipid 
inner join mastermerchant mm on md.merchantid = mm.merchantid 
inner join mastershipment ms on t.shipmentid = ms.shipmentid 
 WHERE ' || filters || '
group by mm.merchantid,mm.merchantname,md.dropshipid,md."label",ms.shipmentid,ms.keterangan
 Order By ms.shipmentid';
 
 return query EXECUTE sql; 
 
 END IF;


END;
$$;


ALTER FUNCTION public.grafik_getdatapengirimanperdropshipbydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: grafik_getomsetpertanggalpobydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getomsetpertanggalpobydynamicfilters(filters character varying) RETURNS TABLE(merchantid integer, merchantname character varying, tglpo character varying, total numeric)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 

begin
	if filters='' then 
    return query select * from grafik_getallomsetpertanggalpo();
ELSE
    sql = 'select mm.merchantid,mm.merchantname, date(toph.openpodate)::varchar tglpo,sum(t.total) total 
from transorderheader t
inner join transopenpoheader toph on t.openpoheaderid = toph.openpoheaderid
inner join mastermerchant mm on toph.merchantid = mm.merchantid 
where ' || filters || '
group by mm.merchantid,date(toph.openpodate)
order by date(toph.openpodate)';

 return query EXECUTE sql; 
end if;

end;
$$;


ALTER FUNCTION public.grafik_getomsetpertanggalpobydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: grafik_getongkoskirimperdropshipbydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getongkoskirimperdropshipbydynamicfilters(filters character varying) RETURNS TABLE(merchantid integer, merchantname character varying, dropshipid integer, label character varying, totalongkoskirim numeric)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from grafik_getalldataongkoskirimperdropship();
ELSE
    sql = 'select 
mm.merchantid,mm.merchantname,md.dropshipid,md.label, sum(topdd.ongkoskirim) totalongkoskirim 
from transorderheader t
inner join transopenpodetaildropship topdd on t.openpodetaildropshipid = topdd.openpodetaildropshipid
inner join masterdropship md on topdd.dropshipid = md.dropshipid 
inner join mastermerchant mm on md.merchantid = mm.merchantid
 WHERE ' || filters || '
group by mm.merchantid,md.dropshipid
 Order By md.dropshipid';
 
 return query EXECUTE sql; 
 
 END IF;

    
END;
$$;


ALTER FUNCTION public.grafik_getongkoskirimperdropshipbydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: grafik_getqtyproductterjualbyweekormonth(character varying, integer, integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.grafik_getqtyproductterjualbyweekormonth(p_filter character varying, p_minggu integer, p_bulan integer, p_tahun integer) RETURNS TABLE(tahun integer, bulan integer, minggu integer, merchantid integer, merchantname character varying, productid integer, productname character varying, productsatuan character varying, qtyterjual numeric)
    LANGUAGE plpgsql
    AS $$
begin 
	if p_filter = 'week' then
	
	return query select date_part('year',th.waktuentry)::int tahun,date_part('month',th.waktuentry)::int bulan,
	getweekfromdate(th.waktuentry) minggu, 
mm.merchantid,mm.merchantname ,mp.productid,mp.productname,mp.productsatuan, sum(td.qty) qtyterjual
from 
transorderheader th
inner join transorderdetail td on th.orderheaderid = td.orderheaderid 
inner join transopenpodetailproduct dp on td.openpodetailproductid = dp.openpodetailproductid 
inner join masterproduct mp on dp.productid = mp.productid
inner join mastermerchant mm on mp.merchantid = mm.merchantid 
where date_part('month',th.waktuentry) = p_bulan 
and date_part('year',th.waktuentry) = p_tahun
and getweekfromdate(th.waktuentry) = p_minggu

group by mm.merchantid,mp.productid,
date_part('month',th.waktuentry),
date_part('year',th.waktuentry),
getweekfromdate(th.waktuentry),mp.productsatuan 
order by mm.merchantid;

else

return query select date_part('year',th.waktuentry)::int tahun,date_part('month',th.waktuentry)::int bulan,
	0 minggu, 
mm.merchantid,mm.merchantname ,mp.productid,mp.productname,mp.productsatuan, sum(td.qty) qtyterjual
from 
transorderheader th
inner join transorderdetail td on th.orderheaderid = td.orderheaderid 
inner join transopenpodetailproduct dp on td.openpodetailproductid = dp.openpodetailproductid 
inner join masterproduct mp on dp.productid = mp.productid
inner join mastermerchant mm on mp.merchantid = mm.merchantid 
where date_part('month',th.waktuentry) = p_bulan and date_part('year',th.waktuentry) = p_tahun
group by mm.merchantid,mp.productid,
date_part('month',th.waktuentry),
date_part('year',th.waktuentry),
mp.productsatuan 
order by mm.merchantid;

end if;

end;
$$;


ALTER FUNCTION public.grafik_getqtyproductterjualbyweekormonth(p_filter character varying, p_minggu integer, p_bulan integer, p_tahun integer) OWNER TO postgres;

--
-- Name: mastercategorymenu_deletedata(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.mastercategorymenu_deletedata(p_categorymenuid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int; 

BEGIN

DELETE FROM mastercategorymenu 
 WHERE categorymenuid=p_categorymenuid;

 GET DIAGNOSTICS rcount = ROW_COUNT; 

if(rcount > 0) then
    return query select 1;
else
    return query select 0;
end if;

    
END;
$$;


ALTER FUNCTION public.mastercategorymenu_deletedata(p_categorymenuid integer) OWNER TO postgres;

--
-- Name: mastercategorymenu_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.mastercategorymenu_getalldata() RETURNS TABLE(categorymenuid integer, categorymenuname character varying, categoryimageurl character varying)
    LANGUAGE plpgsql
    AS $$

BEGIN

return query 
 SELECT 
m.categorymenuid,
m.categorymenuname,
m.categoryimageurl
FROM mastercategorymenu m;



    
END;
$$;


ALTER FUNCTION public.mastercategorymenu_getalldata() OWNER TO postgres;

--
-- Name: mastercategorymenu_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.mastercategorymenu_getdatabydynamicfilters(filters character varying) RETURNS TABLE(categorymenuid integer, categorymenuname character varying, categoryimageurl character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from mastercategorymenu_GetAllData();
ELSE
    sql = 'SELECT 
m.categorymenuid,
m.categorymenuname,
m.categoryimageurl
FROM mastercategorymenu m
 WHERE ' || filters || '
 Order By categorymenuid';
 
 return query EXECUTE sql; 
 
 END IF;



    
END;
$$;


ALTER FUNCTION public.mastercategorymenu_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: mastercategorymenu_insertdata(character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.mastercategorymenu_insertdata(p_categorymenuname character varying, p_categoryimageurl character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int; 

BEGIN

INSERT INTO mastercategorymenu 
(
categorymenuname,
categoryimageurl
) 
 
 VALUES(
p_categorymenuname,
p_categoryimageurl
);

 GET DIAGNOSTICS rcount = ROW_COUNT; 


if(rcount > 0) then
    return query select 1;
else
    return query select 0;
end if;

    
END;
$$;


ALTER FUNCTION public.mastercategorymenu_insertdata(p_categorymenuname character varying, p_categoryimageurl character varying) OWNER TO postgres;

--
-- Name: mastercategorymenu_updatedata(integer, character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.mastercategorymenu_updatedata(p_categorymenuid integer, p_categorymenuname character varying, p_categoryimageurl character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int; 

BEGIN

UPDATE mastercategorymenu set 
categorymenuname=p_categorymenuname,
categoryimageurl=p_categoryimageurl
 
 WHERE categorymenuid=p_categorymenuid;

 GET DIAGNOSTICS rcount = ROW_COUNT; 


if(rcount > 0) then
    return query select 1;
else
    return query select 0;
end if;

    
END;
$$;


ALTER FUNCTION public.mastercategorymenu_updatedata(p_categorymenuid integer, p_categorymenuname character varying, p_categoryimageurl character varying) OWNER TO postgres;

--
-- Name: mastercounter_insertupdate(character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.mastercounter_insertupdate(p_prefix character varying, p_jenistransaksi character varying) RETURNS TABLE(hasil character varying)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 transid varchar = ''; 

BEGIN

if not exists(select 1 from mastercounter where prefix = p_prefix limit 1 ) then
INSERT INTO mastercounter 
(
prefix,
jenistransaksi,
lastinc,
lastid
) 
 
 VALUES(
UPPER(p_prefix),
UPPER(p_jenistransaksi),
1,
p_prefix || CAST(right('00000000001',10) AS VARCHAR) 
) returning lastid INTO transid;
else 
update mastercounter    
set lastid=UPPER(p_prefix) || CAST(RIGHT('0000000000'||CAST((select lastinc + 1 from mastercounter where prefix = p_prefix limit 1) AS VARCHAR(10)),10) AS VARCHAR),    
lastinc=lastinc+1    
where prefix=p_prefix returning lastid INTO transid; 

end if; 

return query select transid;
    
END;
$$;


ALTER FUNCTION public.mastercounter_insertupdate(p_prefix character varying, p_jenistransaksi character varying) OWNER TO postgres;

--
-- Name: masterdropship_deletedata(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterdropship_deletedata(p_dropshipid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int; 

BEGIN

DELETE FROM masterdropship 
 
 WHERE dropshipid=p_dropshipid;

 GET DIAGNOSTICS rcount = ROW_COUNT; 


if(rcount > 0) then
    return query select 1;
else
    return query select 0;
end if;

    
END;
$$;


ALTER FUNCTION public.masterdropship_deletedata(p_dropshipid integer) OWNER TO postgres;

--
-- Name: masterdropship_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterdropship_getalldata() RETURNS TABLE(dropshipid integer, merchantid integer, label character varying, longitude character varying, latitude character varying, dropshipaddress character varying, description character varying, contactname character varying, contactphone character varying, radius numeric, isactive boolean, ongkoskirim numeric, iscod boolean)
    LANGUAGE plpgsql
    AS $$

BEGIN

return query 
 SELECT 
m.dropshipid,
m.merchantid,
m.label,
m.longitude,
m.latitude,
m.dropshipaddress,
m.description,
m.contactname,
m.contactphone,
m.radius,
m.isactive,
m.ongkoskirim,
m.iscod
FROM masterdropship m;
    
END;
$$;


ALTER FUNCTION public.masterdropship_getalldata() OWNER TO postgres;

--
-- Name: masterdropship_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterdropship_getdatabydynamicfilters(filters character varying) RETURNS TABLE(dropshipid integer, merchantid integer, label character varying, longitude character varying, latitude character varying, dropshipaddress character varying, description character varying, contactname character varying, contactphone character varying, radius numeric, isactive boolean, ongkoskirim numeric, iscod boolean)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from masterdropship_GetAllData();
ELSE
    sql = 'SELECT 
m.dropshipid,
m.merchantid,
m.label,
m.longitude,
m.latitude,
m.dropshipaddress,
m.description,
m.contactname,
m.contactphone,
m.radius,
m.isactive,
m.ongkoskirim,
m.iscod
FROM masterdropship m
 WHERE ' || filters || '
 Order By dropshipid';
 
 return query EXECUTE sql; 
 
 END IF;

    
END;
$$;


ALTER FUNCTION public.masterdropship_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: masterdropship_insertdata(integer, character varying, character varying, character varying, character varying, character varying, character varying, character varying, numeric, boolean, numeric, boolean); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterdropship_insertdata(p_merchantid integer, p_label character varying, p_longitude character varying, p_latitude character varying, p_dropshipaddress character varying, p_description character varying, p_contactname character varying, p_contactphone character varying, p_radius numeric, p_isactive boolean, p_ongkoskirim numeric, p_iscod boolean) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

INSERT INTO masterdropship 
(
merchantid,
label,
longitude,
latitude,
dropshipaddress,
description,
contactname,
contactphone,
radius,
isactive,
ongkoskirim,
iscod
) 
 
 VALUES(
p_merchantid,
p_label,
p_longitude,
p_latitude,
p_dropshipaddress,
p_description,
p_contactname,
p_contactphone,
p_radius,
p_isactive,
p_ongkoskirim,
p_iscod
) RETURNING dropshipid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.masterdropship_insertdata(p_merchantid integer, p_label character varying, p_longitude character varying, p_latitude character varying, p_dropshipaddress character varying, p_description character varying, p_contactname character varying, p_contactphone character varying, p_radius numeric, p_isactive boolean, p_ongkoskirim numeric, p_iscod boolean) OWNER TO postgres;

--
-- Name: masterdropship_updatedata(integer, integer, character varying, character varying, character varying, character varying, character varying, character varying, character varying, numeric, boolean, numeric, boolean); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterdropship_updatedata(p_dropshipid integer, p_merchantid integer, p_label character varying, p_longitude character varying, p_latitude character varying, p_dropshipaddress character varying, p_description character varying, p_contactname character varying, p_contactphone character varying, p_radius numeric, p_isactive boolean, p_ongkoskirim numeric, p_iscod boolean) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

UPDATE masterdropship set 
merchantid=p_merchantid,
label=p_label,
longitude=p_longitude,
latitude=p_latitude,
dropshipaddress=p_dropshipaddress,
description=p_description,
contactname=p_contactname,
contactphone=p_contactphone,
radius=p_radius,
isactive=p_isactive,
ongkoskirim=p_ongkoskirim,
iscod=p_iscod
 
 WHERE dropshipid=p_dropshipid 
RETURNING dropshipid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.masterdropship_updatedata(p_dropshipid integer, p_merchantid integer, p_label character varying, p_longitude character varying, p_latitude character varying, p_dropshipaddress character varying, p_description character varying, p_contactname character varying, p_contactphone character varying, p_radius numeric, p_isactive boolean, p_ongkoskirim numeric, p_iscod boolean) OWNER TO postgres;

--
-- Name: masterdropship_updatestatusactive(integer, boolean); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterdropship_updatestatusactive(p_dropshipid integer, p_isactive boolean) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int; 

BEGIN

UPDATE masterdropship set
isactive=p_isactive
 WHERE dropshipid=p_dropshipid;

 GET DIAGNOSTICS rcount = ROW_COUNT; 

if(rcount > 0) then
    return query select 1;
else
    return query select 0;
end if;

    
END;
$$;


ALTER FUNCTION public.masterdropship_updatestatusactive(p_dropshipid integer, p_isactive boolean) OWNER TO postgres;

--
-- Name: masterkota_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterkota_getalldata() RETURNS TABLE(id integer, idprovinsi integer, nama character varying)
    LANGUAGE plpgsql
    AS $$

BEGIN

return query 
 SELECT 
m.id,
m.idprovinsi,
m.nama
FROM masterkota m;



    
END;
$$;


ALTER FUNCTION public.masterkota_getalldata() OWNER TO postgres;

--
-- Name: masterkota_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterkota_getdatabydynamicfilters(filters character varying) RETURNS TABLE(id integer, idprovinsi integer, nama character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from masterkota_GetAllData();
ELSE
    sql = 'SELECT 
m.id,
m.idprovinsi,
m.nama
FROM masterkota m
 WHERE ' || filters || '
 Order By id';
 
 return query EXECUTE sql; 
 
 END IF;


END;
$$;


ALTER FUNCTION public.masterkota_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: mastermerchant_deletedata(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.mastermerchant_deletedata(p_merchantid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int; 

BEGIN

DELETE FROM mastermerchant 
 
 WHERE merchantid=p_merchantid;

 GET DIAGNOSTICS rcount = ROW_COUNT; 

if(rcount > 0) then
    return query select 1;
else
    return query select 0;
end if;

    
END;
$$;


ALTER FUNCTION public.mastermerchant_deletedata(p_merchantid integer) OWNER TO postgres;

--
-- Name: mastermerchant_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.mastermerchant_getalldata() RETURNS TABLE(merchantid integer, userid integer, merchantname character varying, description character varying, logoimageurl character varying, coverimageurl character varying, avgratingproduct numeric, avgratingpackaging numeric, avgratingdelivering numeric, namabank character varying, nomorrekening character varying, namapemilikrekening character varying, identitycardurl character varying)
    LANGUAGE plpgsql
    AS $$

BEGIN

return query 
 SELECT 
m.merchantid,
m.userid,
m.merchantname,
m.description,
m.logoimageurl,
m.coverimageurl,
m.avgratingproduct,
m.avgratingpackaging,
m.avgratingdelivering,
m.namabank,
m.nomorrekening,
m.namapemilikrekening,
u.identitycardurl

FROM mastermerchant m
inner join users u on m.userid = u.userid ;

    
END;
$$;


ALTER FUNCTION public.mastermerchant_getalldata() OWNER TO postgres;

--
-- Name: mastermerchant_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.mastermerchant_getdatabydynamicfilters(filters character varying) RETURNS TABLE(merchantid integer, userid integer, merchantname character varying, description character varying, logoimageurl character varying, coverimageurl character varying, avgratingproduct numeric, avgratingpackaging numeric, avgratingdelivering numeric, namabank character varying, nomorrekening character varying, namapemilikrekening character varying, identitycardurl character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from mastermerchant_GetAllData();
ELSE
    sql = 'SELECT 
m.merchantid,
m.userid,
m.merchantname,
m.description,
m.logoimageurl,
m.coverimageurl,
m.avgratingproduct,
m.avgratingpackaging,
m.avgratingdelivering,
m.namabank,
m.nomorrekening,
m.namapemilikrekening,
u.identitycardurl
FROM mastermerchant m
inner join users u on m.userid = u.userid 
 WHERE ' || filters || '
 Order By merchantid';
 
 return query EXECUTE sql; 
 
 END IF;



    
END;
$$;


ALTER FUNCTION public.mastermerchant_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: mastermerchant_insertdata(integer, character varying, character varying, character varying, character varying, character varying, character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.mastermerchant_insertdata(p_userid integer, p_merchantname character varying, p_description character varying, p_logoimageurl character varying, p_coverimageurl character varying, p_namabank character varying, p_nomorrekening character varying, p_namapemilikrekening character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

if not exists (select 1 from mastermerchant m2 where userid = p_userid) then
INSERT INTO mastermerchant 
(
userid,
merchantname,
description,
logoimageurl,
coverimageurl,
namabank,
nomorrekening,
namapemilikrekening
) 
 
 VALUES(
p_userid,
p_merchantname,
p_description,
p_logoimageurl,
p_coverimageurl,
p_namabank,
p_nomorrekening,
p_namapemilikrekening
) RETURNING merchantid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
else
return query select -1;
end if;
    
END;
$$;


ALTER FUNCTION public.mastermerchant_insertdata(p_userid integer, p_merchantname character varying, p_description character varying, p_logoimageurl character varying, p_coverimageurl character varying, p_namabank character varying, p_nomorrekening character varying, p_namapemilikrekening character varying) OWNER TO postgres;

--
-- Name: mastermerchant_updatedata(integer, integer, character varying, character varying, character varying, character varying, character varying, character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.mastermerchant_updatedata(p_merchantid integer, p_userid integer, p_merchantname character varying, p_description character varying, p_logoimageurl character varying, p_coverimageurl character varying, p_namabank character varying, p_nomorrekening character varying, p_namapemilikrekening character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

UPDATE mastermerchant set 
userid=p_userid,
merchantname=p_merchantname,
description=p_description,
logoimageurl=p_logoimageurl,
coverimageurl=p_coverimageurl,
namabank=p_namabank,
nomorrekening=p_nomorrekening,
namapemilikrekening=p_namapemilikrekening
 
 WHERE merchantid=p_merchantid 
RETURNING merchantid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.mastermerchant_updatedata(p_merchantid integer, p_userid integer, p_merchantname character varying, p_description character varying, p_logoimageurl character varying, p_coverimageurl character varying, p_namabank character varying, p_nomorrekening character varying, p_namapemilikrekening character varying) OWNER TO postgres;

--
-- Name: mastermerchant_updaterating(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.mastermerchant_updaterating(p_merchantid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 
 
 rating_product decimal = 0;
 rating_deliver decimal = 0;
 rating_package decimal = 0;

BEGIN

rating_product := (select coalesce(round(sum(rating)::numeric/count(1)::numeric,2),0) from masterratingproduct where merchantid=p_merchantid);
rating_deliver := (select coalesce(round(sum(rating)::numeric/count(1)::numeric,2),0) from masterratingdelivering where merchantid=p_merchantid);
rating_package := (select coalesce(round(sum(rating)::numeric/count(1)::numeric,2),0) from masterratingpackaging where merchantid=p_merchantid);


UPDATE mastermerchant set 
avgratingproduct=rating_product,
avgratingpackaging=rating_deliver,
avgratingdelivering=rating_package
 
 WHERE merchantid=p_merchantid 
RETURNING merchantid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.mastermerchant_updaterating(p_merchantid integer) OWNER TO postgres;

--
-- Name: masterpaymentmethod_deletedata(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterpaymentmethod_deletedata(p_paymentmethodid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

DELETE FROM masterpaymentmethod 
 
 WHERE paymentmethodid=p_paymentmethodid 
RETURNING paymentmethodid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.masterpaymentmethod_deletedata(p_paymentmethodid integer) OWNER TO postgres;

--
-- Name: masterpaymentmethod_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterpaymentmethod_getalldata() RETURNS TABLE(paymentmethodid integer, keterangan character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN

return query 
 SELECT 
m.paymentmethodid,
m.keterangan
FROM masterpaymentmethod m;

    
END;
$$;


ALTER FUNCTION public.masterpaymentmethod_getalldata() OWNER TO postgres;

--
-- Name: masterpaymentmethod_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterpaymentmethod_getdatabydynamicfilters(filters character varying) RETURNS TABLE(paymentmethodid integer, keterangan character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from masterpaymentmethod_GetAllData();
ELSE
    sql = 'SELECT 
m.paymentmethodid,
m.keterangan
FROM masterpaymentmethod m
 WHERE ' || filters || '
 Order By paymentmethodid';
 
 return query EXECUTE sql; 
 
 END IF;

END;
$$;


ALTER FUNCTION public.masterpaymentmethod_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: masterpaymentmethod_insertdata(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterpaymentmethod_insertdata(p_keterangan character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

INSERT INTO masterpaymentmethod 
(
keterangan
) 
 
 VALUES(
p_keterangan
) RETURNING paymentmethodid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.masterpaymentmethod_insertdata(p_keterangan character varying) OWNER TO postgres;

--
-- Name: masterpaymentmethod_updatedata(integer, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterpaymentmethod_updatedata(p_paymentmethodid integer, p_keterangan character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

UPDATE masterpaymentmethod set 
keterangan=p_keterangan
 
 WHERE paymentmethodid=p_paymentmethodid 
RETURNING paymentmethodid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.masterpaymentmethod_updatedata(p_paymentmethodid integer, p_keterangan character varying) OWNER TO postgres;

--
-- Name: masterproduct_deletedata(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterproduct_deletedata(p_productid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int; 

BEGIN

DELETE FROM masterproduct 
 
 WHERE productid=p_productid;

 GET DIAGNOSTICS rcount = ROW_COUNT; 

if(rcount > 0) then
    return query select 1;
else
    return query select 0;
end if;

    
END;
$$;


ALTER FUNCTION public.masterproduct_deletedata(p_productid integer) OWNER TO postgres;

--
-- Name: masterproduct_getalldataformarket(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterproduct_getalldataformarket() RETURNS TABLE(productid integer, merchantid integer, merchantname character varying, categorymenuid integer, productname character varying, productimageurl character varying, productprice numeric, productdescription character varying, isbutton boolean, isactive boolean, productsatuan character varying, kuota numeric, kuotamax numeric, kuotamin numeric, qtymax numeric, qtymin numeric, ishalal boolean)
    LANGUAGE plpgsql
    AS $$
BEGIN

return query 
 SELECT 
m.productid,
m.merchantid,
mm.merchantname,
m.categorymenuid,
m.productname,
m.productimageurl,
m.productprice,
m.productdescription,
m.isbutton,
m.isactive,
m.productsatuan,
m.kuota,
m.kuotamax,
m.kuotamin,
m.qtymax,
m.qtymin,
m.ishalal
FROM masterproduct m
inner join mastermerchant mm on m.merchantid = mm.merchantid ;

END;
$$;


ALTER FUNCTION public.masterproduct_getalldataformarket() OWNER TO postgres;

--
-- Name: masterproduct_getalldataforuser(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterproduct_getalldataforuser() RETURNS TABLE(productid integer, merchantid integer, categorymenuid integer, productname character varying, productimageurl character varying, productprice numeric, productdescription character varying, isbutton boolean, isactive boolean, productsatuan character varying, kuota numeric, kuotamax numeric, kuotamin numeric, qtymax numeric, qtymin numeric, ishalal boolean, isfavorit boolean, userid integer)
    LANGUAGE plpgsql
    AS $$
BEGIN

return query 
 SELECT 
m.productid,
m.merchantid,
m.categorymenuid,
m.productname,
m.productimageurl,
m.productprice,
m.productdescription,
m.isbutton,
m.isactive,
m.productsatuan,
m.kuota,
m.kuotamax,
m.kuotamin,
m.qtymax,
m.qtymin,
m.ishalal,
case
when coalesce(fp.productid::varchar,'') <> '' then true
else false
end isfavorit,
fp.userid
FROM masterproduct m 
left join (select fp.productid,u.userid from 
favoritproduct fp 
left join users u on fp.userid = u.userid) fp on m.productid = fp.productid;
  
END;
$$;


ALTER FUNCTION public.masterproduct_getalldataforuser() OWNER TO postgres;

--
-- Name: masterproduct_getdataformarketbydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterproduct_getdataformarketbydynamicfilters(filters character varying) RETURNS TABLE(productid integer, merchantid integer, merchantname character varying, categorymenuid integer, productname character varying, productimageurl character varying, productprice numeric, productdescription character varying, isbutton boolean, isactive boolean, productsatuan character varying, kuota numeric, kuotamax numeric, kuotamin numeric, qtymax numeric, qtymin numeric, ishalal boolean)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from masterproduct_GetAllDataformarket();
ELSE
    sql = 'SELECT 
m.productid,
mm.merchantid,
mm.merchantname,
m.categorymenuid,
m.productname,
m.productimageurl,
m.productprice,
m.productdescription,
m.isbutton,
m.isactive,
m.productsatuan,
m.kuota,
m.kuotamax,
m.kuotamin,
m.qtymax,
m.qtymin,
m.ishalal
FROM masterproduct m
inner join mastermerchant mm on m.merchantid = mm.merchantid 
 WHERE ' || filters || '
 Order By productid';
 
 return query EXECUTE sql; 
 
 END IF;
  
END;
$$;


ALTER FUNCTION public.masterproduct_getdataformarketbydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: masterproduct_getdataforuserbydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterproduct_getdataforuserbydynamicfilters(filters character varying) RETURNS TABLE(productid integer, merchantid integer, categorymenuid integer, productname character varying, productimageurl character varying, productprice numeric, productdescription character varying, isbutton boolean, isactive boolean, productsatuan character varying, kuota numeric, kuotamax numeric, kuotamin numeric, qtymax numeric, qtymin numeric, ishalal boolean, isfavorit boolean, userid integer)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from masterproduct_GetAllDataForUser();
ELSE
    sql = 'SELECT 
m.productid,
m.merchantid,
m.categorymenuid,
m.productname,
m.productimageurl,
m.productprice,
m.productdescription,
m.isbutton,
m.isactive,
m.productsatuan,
m.kuota,
m.kuotamax,
m.kuotamin,
m.qtymax,
m.qtymin,
m.ishalal,
case
when coalesce(fp.productid::varchar ,'''') <> '''' then true
else false
end isfavorit,
fp.userid
FROM masterproduct m 
left join (select fp.productid,u.userid from 
favoritproduct fp 
left join users u on fp.userid = u.userid) fp on m.productid = fp.productid
 WHERE fp.userid is null or ' || filters || '
 Order By m.productid';
 
 return query EXECUTE sql; 
 
 END IF;

END;
$$;


ALTER FUNCTION public.masterproduct_getdataforuserbydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: masterproduct_insertdata(integer, integer, character varying, character varying, numeric, character varying, boolean, boolean, character varying, numeric, numeric, numeric, numeric, numeric, boolean); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterproduct_insertdata(p_merchantid integer, p_categorymenuid integer, p_productname character varying, p_productimageurl character varying, p_productprice numeric, p_productdescription character varying, p_isbutton boolean, p_isactive boolean, p_productsatuan character varying, p_kuota numeric, p_kuotamax numeric, p_kuotamin numeric, p_qtymax numeric, p_qtymin numeric, p_ishalal boolean) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

INSERT INTO masterproduct 
(
merchantid,
categorymenuid,
productname,
productimageurl,
productprice,
productdescription,
isbutton,
isactive,
productsatuan,
kuota,
kuotamax,
kuotamin,
qtymax,
qtymin,
	ishalal
) 
 
 VALUES(
p_merchantid,
p_categorymenuid,
p_productname,
p_productimageurl,
p_productprice,
p_productdescription,
p_isbutton,
p_isactive,
p_productsatuan,
p_kuota,
p_kuotamax,
p_kuotamin,
p_qtymax,
p_qtymin,
	 p_ishalal
) RETURNING productid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.masterproduct_insertdata(p_merchantid integer, p_categorymenuid integer, p_productname character varying, p_productimageurl character varying, p_productprice numeric, p_productdescription character varying, p_isbutton boolean, p_isactive boolean, p_productsatuan character varying, p_kuota numeric, p_kuotamax numeric, p_kuotamin numeric, p_qtymax numeric, p_qtymin numeric, p_ishalal boolean) OWNER TO postgres;

--
-- Name: masterproduct_updatedata(integer, integer, integer, character varying, character varying, numeric, character varying, boolean, boolean, character varying, numeric, numeric, numeric, numeric, numeric, boolean); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterproduct_updatedata(p_productid integer, p_merchantid integer, p_categorymenuid integer, p_productname character varying, p_productimageurl character varying, p_productprice numeric, p_productdescription character varying, p_isbutton boolean, p_isactive boolean, p_productsatuan character varying, p_kuota numeric, p_kuotamax numeric, p_kuotamin numeric, p_qtymax numeric, p_qtymin numeric, p_ishalal boolean) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

UPDATE masterproduct set 
merchantid=p_merchantid,
categorymenuid=p_categorymenuid,
productname=p_productname,
productimageurl=p_productimageurl,
productprice=p_productprice,
productdescription=p_productdescription,
isbutton=p_isbutton,
isactive=p_isactive,
productsatuan=p_productsatuan,
kuota=p_kuota,
kuotamax=p_kuotamax,
kuotamin=p_kuotamin,
qtymax=p_qtymax,
qtymin=p_qtymin,
ishalal=p_ishalal
 
 WHERE productid=p_productid 
RETURNING productid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.masterproduct_updatedata(p_productid integer, p_merchantid integer, p_categorymenuid integer, p_productname character varying, p_productimageurl character varying, p_productprice numeric, p_productdescription character varying, p_isbutton boolean, p_isactive boolean, p_productsatuan character varying, p_kuota numeric, p_kuotamax numeric, p_kuotamin numeric, p_qtymax numeric, p_qtymin numeric, p_ishalal boolean) OWNER TO postgres;

--
-- Name: masterproduct_updatestatusactive(integer, boolean); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterproduct_updatestatusactive(p_productid integer, p_isactive boolean) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int; 

BEGIN

UPDATE masterproduct set
isactive=p_isactive
 
 WHERE productid=p_productid;

 GET DIAGNOSTICS rcount = ROW_COUNT; 


if(rcount > 0) then
    return query select 1;
else
    return query select 0;
end if;

    
END;
$$;


ALTER FUNCTION public.masterproduct_updatestatusactive(p_productid integer, p_isactive boolean) OWNER TO postgres;

--
-- Name: masterprovinsi_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterprovinsi_getalldata() RETURNS TABLE(id integer, nama character varying)
    LANGUAGE plpgsql
    AS $$

BEGIN

return query 
 SELECT 
m.id,
m.nama
FROM masterprovinsi m;
 
END;
$$;


ALTER FUNCTION public.masterprovinsi_getalldata() OWNER TO postgres;

--
-- Name: masterprovinsi_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterprovinsi_getdatabydynamicfilters(filters character varying) RETURNS TABLE(id integer, nama character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from masterprovinsi_GetAllData();
ELSE
    sql = 'SELECT 
m.id,
m.nama
FROM masterprovinsi m
 WHERE ' || filters || '
 Order By id';
 
 return query EXECUTE sql; 
 
 END IF;

END;
$$;


ALTER FUNCTION public.masterprovinsi_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: masterratingdelivering_insertdata(integer, integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterratingdelivering_insertdata(p_rating integer, p_userentry integer, p_merchantid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

INSERT INTO masterratingdelivering 
(
rating,
userentry,
waktuentry,
merchantid
) 
 
 VALUES(
p_rating,
p_userentry,
current_timestamp,
p_merchantid
) RETURNING ratingdeliveringid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.masterratingdelivering_insertdata(p_rating integer, p_userentry integer, p_merchantid integer) OWNER TO postgres;

--
-- Name: masterratingpackaging_insertdata(integer, integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterratingpackaging_insertdata(p_rating integer, p_userentry integer, p_merchantid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

INSERT INTO masterratingpackaging 
(
rating,
userentry,
waktuentry,
merchantid
) 
 
 VALUES(
p_rating,
p_userentry,
current_timestamp,
p_merchantid
) RETURNING ratingpackagingid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.masterratingpackaging_insertdata(p_rating integer, p_userentry integer, p_merchantid integer) OWNER TO postgres;

--
-- Name: masterratingproduct_insertdata(integer, integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterratingproduct_insertdata(p_rating integer, p_userentry integer, p_merchantid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

INSERT INTO masterratingproduct 
(
rating,
userentry,
waktuentry,
merchantid
) 
 
 VALUES(
p_rating,
p_userentry,
current_timestamp,
p_merchantid
) RETURNING ratingproductid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.masterratingproduct_insertdata(p_rating integer, p_userentry integer, p_merchantid integer) OWNER TO postgres;

--
-- Name: masterrekening_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterrekening_getalldata() RETURNS TABLE(idrekening integer, norekening character varying, namapemilikrekening character varying, namabank character varying)
    LANGUAGE plpgsql
    AS $$

BEGIN

return query 
 SELECT 
m.idrekening,
m.norekening,
m.namapemilikrekening,
m.namabank
FROM masterrekening m;

END;
$$;


ALTER FUNCTION public.masterrekening_getalldata() OWNER TO postgres;

--
-- Name: masterrekening_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterrekening_getdatabydynamicfilters(filters character varying) RETURNS TABLE(idrekening integer, norekening character varying, namapemilikrekening character varying, namabank character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from masterrekening_GetAllData();
ELSE
    sql = 'SELECT 
m.idrekening,
m.norekening,
m.namapemilikrekening,
m.namabank
FROM masterrekening m
 WHERE ' || filters || '
 Order By idrekening';
 
 return query EXECUTE sql; 
 
 END IF;



    
END;
$$;


ALTER FUNCTION public.masterrekening_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: masterrekening_insertdata(character varying, character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterrekening_insertdata(p_norekening character varying, p_namapemilikrekening character varying, p_namabank character varying) RETURNS TABLE(hasil bigint)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

INSERT INTO masterrekening 
(
norekening,
namapemilikrekening,
namabank
) 
 
 VALUES(
p_norekening,
p_namapemilikrekening,
p_namabank
) RETURNING idrekening INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid::bigint;
    
END;
$$;


ALTER FUNCTION public.masterrekening_insertdata(p_norekening character varying, p_namapemilikrekening character varying, p_namabank character varying) OWNER TO postgres;

--
-- Name: mastershipment_deletedata(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.mastershipment_deletedata(p_shipmentid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

DELETE FROM mastershipment 
 
 WHERE shipmentid=p_shipmentid 
RETURNING shipmentid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.mastershipment_deletedata(p_shipmentid integer) OWNER TO postgres;

--
-- Name: mastershipment_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.mastershipment_getalldata() RETURNS TABLE(shipmentid integer, keterangan character varying)
    LANGUAGE plpgsql
    AS $$

BEGIN

return query 
 SELECT 
m.shipmentid,
m.keterangan
FROM mastershipment m;


END;
$$;


ALTER FUNCTION public.mastershipment_getalldata() OWNER TO postgres;

--
-- Name: mastershipment_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.mastershipment_getdatabydynamicfilters(filters character varying) RETURNS TABLE(shipmentid integer, keterangan character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from mastershipment_GetAllData();
ELSE
    sql = 'SELECT 
m.shipmentid,
m.keterangan
FROM mastershipment m
 WHERE ' || filters || '
 Order By shipmentid';
 
 return query EXECUTE sql; 
 
 END IF;

 
END;
$$;


ALTER FUNCTION public.mastershipment_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: mastershipment_insertdata(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.mastershipment_insertdata(p_keterangan character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

INSERT INTO mastershipment 
(
keterangan
) 
 
 VALUES(
p_keterangan
) RETURNING shipmentid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.mastershipment_insertdata(p_keterangan character varying) OWNER TO postgres;

--
-- Name: mastershipment_updatedata(integer, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.mastershipment_updatedata(p_shipmentid integer, p_keterangan character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

UPDATE mastershipment set 
keterangan=p_keterangan
 
 WHERE shipmentid=p_shipmentid 
RETURNING shipmentid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.mastershipment_updatedata(p_shipmentid integer, p_keterangan character varying) OWNER TO postgres;

--
-- Name: masterstatuspembayaran_deletedata(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterstatuspembayaran_deletedata(p_statuspembayaranid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

DELETE FROM masterstatuspembayaran 
 
 WHERE statuspembayaranid=p_statuspembayaranid 
RETURNING statuspembayaranid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.masterstatuspembayaran_deletedata(p_statuspembayaranid integer) OWNER TO postgres;

--
-- Name: masterstatuspembayaran_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterstatuspembayaran_getalldata() RETURNS TABLE(statuspembayaranid integer, keterangan character varying)
    LANGUAGE plpgsql
    AS $$

BEGIN

return query 
 SELECT 
m.statuspembayaranid,
m.keterangan
FROM masterstatuspembayaran m;



    
END;
$$;


ALTER FUNCTION public.masterstatuspembayaran_getalldata() OWNER TO postgres;

--
-- Name: masterstatuspembayaran_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterstatuspembayaran_getdatabydynamicfilters(filters character varying) RETURNS TABLE(statuspembayaranid integer, keterangan character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from masterstatuspembayaran_GetAllData();
ELSE
    sql = 'SELECT 
m.statuspembayaranid,
m.keterangan
FROM masterstatuspembayaran m
 WHERE ' || filters || '
 Order By statuspembayaranid';
 
 return query EXECUTE sql; 
 
 END IF;



    
END;
$$;


ALTER FUNCTION public.masterstatuspembayaran_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: masterstatuspembayaran_insertdata(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterstatuspembayaran_insertdata(p_keterangan character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

INSERT INTO masterstatuspembayaran 
(
keterangan
) 
 
 VALUES(
p_keterangan
) RETURNING statuspembayaranid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.masterstatuspembayaran_insertdata(p_keterangan character varying) OWNER TO postgres;

--
-- Name: masterstatuspembayaran_updatedata(integer, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterstatuspembayaran_updatedata(p_statuspembayaranid integer, p_keterangan character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

UPDATE masterstatuspembayaran set 
keterangan=p_keterangan
 
 WHERE statuspembayaranid=p_statuspembayaranid 
RETURNING statuspembayaranid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.masterstatuspembayaran_updatedata(p_statuspembayaranid integer, p_keterangan character varying) OWNER TO postgres;

--
-- Name: masterstatustransaksiorder_deletedata(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterstatustransaksiorder_deletedata(p_statustransorderid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

DELETE FROM masterstatustransaksiorder 
 
 WHERE statustransorderid=p_statustransorderid 
RETURNING statustransorderid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.masterstatustransaksiorder_deletedata(p_statustransorderid integer) OWNER TO postgres;

--
-- Name: masterstatustransaksiorder_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterstatustransaksiorder_getalldata() RETURNS TABLE(statustransorderid integer, keterangan character varying)
    LANGUAGE plpgsql
    AS $$

BEGIN

return query 
 SELECT 
m.statustransorderid,
m.keterangan
FROM masterstatustransaksiorder m;


END;
$$;


ALTER FUNCTION public.masterstatustransaksiorder_getalldata() OWNER TO postgres;

--
-- Name: masterstatustransaksiorder_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterstatustransaksiorder_getdatabydynamicfilters(filters character varying) RETURNS TABLE(statustransorderid integer, keterangan character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from masterstatustransaksiorder_GetAllData();
ELSE
    sql = 'SELECT 
m.statustransorderid,
m.keterangan
FROM masterstatustransaksiorder m
 WHERE ' || filters || '
 Order By statustransorderid';
 
 return query EXECUTE sql; 
 
 END IF;
    
END;
$$;


ALTER FUNCTION public.masterstatustransaksiorder_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: masterstatustransaksiorder_insertdata(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterstatustransaksiorder_insertdata(p_keterangan character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

INSERT INTO masterstatustransaksiorder 
(
keterangan
) 
 
 VALUES(
p_keterangan
) RETURNING statustransorderid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.masterstatustransaksiorder_insertdata(p_keterangan character varying) OWNER TO postgres;

--
-- Name: masterstatustransaksiorder_updatedata(integer, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.masterstatustransaksiorder_updatedata(p_statustransorderid integer, p_keterangan character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

UPDATE masterstatustransaksiorder set 
keterangan=p_keterangan
 
 WHERE statustransorderid=p_statustransorderid 
RETURNING statustransorderid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.masterstatustransaksiorder_updatedata(p_statustransorderid integer, p_keterangan character varying) OWNER TO postgres;

--
-- Name: product_getalldataperingkatproduct(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.product_getalldataperingkatproduct() RETURNS TABLE(merchantid integer, merchantname character varying, productid integer, productname character varying, qty bigint)
    LANGUAGE plpgsql
    AS $$
begin
return query select mm.merchantid,mm.merchantname, m.productid,m.productname ,count(f.productid) Qty
from masterproduct m
left join transopenpodetailproduct topdp on m.productid = topdp.productid 
left join transopenpodetaildropship topdd on topdp.openpodetaildropshipid = topdd.openpodetaildropshipid 
left join favoritproduct f on m.productid = f.productid 
left join mastermerchant mm on m.merchantid = mm.merchantid 
group by m.productid,m.productname,mm.merchantid ,mm.merchantname 
order by m.productid;

end;
$$;


ALTER FUNCTION public.product_getalldataperingkatproduct() OWNER TO postgres;

--
-- Name: product_getdataperingkatproductbydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.product_getdataperingkatproductbydynamicfilters(filters character varying) RETURNS TABLE(merchantid integer, merchantname character varying, productid integer, productname character varying, qty bigint)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from product_getalldataperingkatproduct();
ELSE
	sql = ' select mm.merchantid,mm.merchantname, m.productid,m.productname ,count(f.productid) Qty
from masterproduct m
left join transopenpodetailproduct topdp on m.productid = topdp.productid 
left join transopenpodetaildropship topdd on topdp.openpodetaildropshipid = topdd.openpodetaildropshipid 
left join favoritproduct f on m.productid = f.productid 
left join mastermerchant mm on m.merchantid = mm.merchantid 

where ' || filters || '
group by m.productid,m.productname,mm.merchantid ,mm.merchantname 
order by m.productid';

 return query EXECUTE sql; 
end if;
end;
$$;


ALTER FUNCTION public.product_getdataperingkatproductbydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: promomenu_deletedata(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.promomenu_deletedata(p_promomenuid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int; 

BEGIN

DELETE FROM promomenu 
 
 WHERE promomenuid=p_promomenuid;

 GET DIAGNOSTICS rcount = ROW_COUNT; 


if(rcount > 0) then
    return query select 1;
else
    return query select 0;
end if;

    
END;
$$;


ALTER FUNCTION public.promomenu_deletedata(p_promomenuid integer) OWNER TO postgres;

--
-- Name: promomenu_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.promomenu_getalldata() RETURNS TABLE(promomenuid integer, productid integer, productname character varying, productimageurl character varying, promoanimationurl character varying, cityid integer, cityname character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN

return query 
 SELECT 
p.promomenuid,
p.productid,
mp.productname,
mp.productimageurl, 
p.promoanimationurl,
p.cityid,
mk.nama cityname
FROM promomenu p 
inner join masterproduct mp on p.productid = mp.productid
left join masterkota mk on p.cityid = mk.id;

END;
$$;


ALTER FUNCTION public.promomenu_getalldata() OWNER TO postgres;

--
-- Name: promomenu_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.promomenu_getdatabydynamicfilters(filters character varying) RETURNS TABLE(promomenuid integer, productid integer, productname character varying, productimageurl character varying, promoanimationurl character varying, cityid integer, cityname character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from promomenu_GetAllData();
ELSE
    sql = 'SELECT 
p.promomenuid,
p.productid,
mp.productname,
mp.productimageurl,
p.promoanimationurl,
p.cityid,
mk.nama cityname
FROM promomenu p
inner join masterproduct mp on p.productid = mp.productid
left join masterkota mk on p.cityid = mk.id
 WHERE ' || filters || '
 Order By promomenuid';
 
 return query EXECUTE sql; 
 
 END IF;

    
END;
$$;


ALTER FUNCTION public.promomenu_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: promomenu_insertdata(integer, character varying, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.promomenu_insertdata(p_productid integer, p_promoanimationurl character varying, p_cityid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

INSERT INTO promomenu 
(
productid,
promoanimationurl,
cityid
) 
 
 VALUES(
p_productid,
p_promoanimationurl,
p_cityid
) RETURNING promomenuid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.promomenu_insertdata(p_productid integer, p_promoanimationurl character varying, p_cityid integer) OWNER TO postgres;

--
-- Name: promomenu_updatedata(integer, integer, character varying, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.promomenu_updatedata(p_promomenuid integer, p_productid integer, p_promoanimationurl character varying, p_cityid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

UPDATE promomenu set 
productid=p_productid,
promoanimationurl=p_promoanimationurl,
cityid=p_cityid
 
 WHERE promomenuid=p_promomenuid 
RETURNING promomenuid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.promomenu_updatedata(p_promomenuid integer, p_productid integer, p_promoanimationurl character varying, p_cityid integer) OWNER TO postgres;

--
-- Name: punishment_deletedata(integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.punishment_deletedata(p_userid integer, p_merchantid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

DELETE FROM punishment 
 
 WHERE userid = p_userid and merchantid=p_merchantid
RETURNING punishmentid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

 return query select lastid; 
  
END;
$$;


ALTER FUNCTION public.punishment_deletedata(p_userid integer, p_merchantid integer) OWNER TO postgres;

--
-- Name: punishment_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.punishment_getalldata() RETURNS TABLE(punishmentid integer, userid integer, firstname character varying, email character varying, avatarurl character varying, merchantid integer, orderheaderid integer)
    LANGUAGE plpgsql
    AS $$
BEGIN

return query 
 SELECT 
p.punishmentid,
p.userid,
u.firstname,
u.email,
u.avatarurl,
p.merchantid,
p.orderheaderid
FROM punishment p
inner join users u on p.userid = u.userid;
    
END;
$$;


ALTER FUNCTION public.punishment_getalldata() OWNER TO postgres;

--
-- Name: punishment_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.punishment_getdatabydynamicfilters(filters character varying) RETURNS TABLE(punishmentid integer, userid integer, firstname character varying, email character varying, avatarurl character varying, merchantid integer, orderheaderid integer)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from punishment_GetAllData();
ELSE
    sql = 'SELECT 
p.punishmentid,
p.userid,
u.firstname,
u.email,
u.avatarurl,
p.merchantid,
p.orderheaderid
FROM punishment p
inner join users u on p.userid = u.userid
 WHERE ' || filters || '
 Order By punishmentid';
 
 return query EXECUTE sql; 
 
 END IF;


END;
$$;


ALTER FUNCTION public.punishment_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: punishment_insertdata(integer, integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.punishment_insertdata(p_userid integer, p_merchantid integer, p_orderheaderid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

INSERT INTO punishment 
(
userid,
merchantid,
orderheaderid
) 
 
 VALUES(
p_userid,
p_merchantid,
p_orderheaderid
) RETURNING punishmentid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.punishment_insertdata(p_userid integer, p_merchantid integer, p_orderheaderid integer) OWNER TO postgres;

--
-- Name: transabsensidropship_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transabsensidropship_getalldata() RETURNS TABLE(openpodetaildropshipid integer, openpoheaderid integer, latitude character varying, longitude character varying, address character varying, foto character varying, waktuentry timestamp without time zone)
    LANGUAGE plpgsql
    AS $$

BEGIN

return query 
 SELECT 
t.openpodetaildropshipid,
t.openpoheaderid,
t.latitude,
t.longitude,
t.address,
t.foto,
t.waktuentry
FROM transabsensidropship t;

END;
$$;


ALTER FUNCTION public.transabsensidropship_getalldata() OWNER TO postgres;

--
-- Name: transabsensidropship_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transabsensidropship_getdatabydynamicfilters(filters character varying) RETURNS TABLE(openpodetaildropshipid integer, openpoheaderid integer, latitude character varying, longitude character varying, address character varying, foto character varying, waktuentry timestamp without time zone)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from transabsensidropship_GetAllData();
ELSE
    sql = 'SELECT 
t.openpodetaildropshipid,
t.openpoheaderid,
t.latitude,
t.longitude,
t.address,
t.foto,
t.waktuentry
FROM transabsensidropship t
 WHERE ' || filters || '
 Order By t.waktuentry';
 
 return query EXECUTE sql; 
 
 END IF;

END;
$$;


ALTER FUNCTION public.transabsensidropship_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: transabsensidropship_insertdata(integer, integer, character varying, character varying, character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transabsensidropship_insertdata(p_openpodetaildropshipid integer, p_openpoheaderid integer, p_latitude character varying, p_longitude character varying, p_address character varying, p_foto character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 
 tglsekarang timestamp = current_timestamp;

BEGIN

	if exists(select 1 from transopenpoheader where closepodate > tglsekarang) then 
	return query select -1;
	else
INSERT INTO transabsensidropship 
(
openpodetaildropshipid,
openpoheaderid,
latitude,
longitude,
address,
foto,
waktuentry
) 
 
 VALUES(
p_openpodetaildropshipid,
p_openpoheaderid,
p_latitude,
p_longitude,
p_address,
p_foto,
	 tglsekarang
) RETURNING openpodetaildropshipid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

if(rcount > 0) then
	return query select lastid;
    else 
	return query select 0;
	end if;
end if;
END;
$$;


ALTER FUNCTION public.transabsensidropship_insertdata(p_openpodetaildropshipid integer, p_openpoheaderid integer, p_latitude character varying, p_longitude character varying, p_address character varying, p_foto character varying) OWNER TO postgres;

--
-- Name: transopenpodetaildropship_deletedata(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transopenpodetaildropship_deletedata(p_openpodetaildropshipid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

DELETE FROM transopenpodetaildropship 
 WHERE openpodetaildropshipid=p_openpodetaildropshipid
RETURNING openpodetaildropshipid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.transopenpodetaildropship_deletedata(p_openpodetaildropshipid integer) OWNER TO postgres;

--
-- Name: transopenpodetaildropship_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transopenpodetaildropship_getalldata() RETURNS TABLE(openpodetaildropshipid integer, openpoheaderid integer, starttime character varying, endtime character varying, tolerance character varying, keterangan character varying, ongkoskirim numeric, iscod boolean, dropshipid integer, dropshipname character varying, dropshipaddress character varying, latitude character varying, longitude character varying, radius numeric, contactname character varying, contactphone character varying, dropshipdesc character varying, merchantid integer, merchantname character varying, merchantdesc character varying, logoimageurl character varying, coverimageurl character varying, categorymenuid integer, openpodate timestamp without time zone, closepodate timestamp without time zone, statustransaksi character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN

return query 
 SELECT 
t.openpodetaildropshipid,
t.openpoheaderid,
t.starttime,
t.endtime,
t.tolerance,
t.keterangan,
t.ongkoskirim,
t.iscod,
md.dropshipid,
md."label" dropshipname,
md.dropshipaddress,
md.latitude,
md.longitude,
md.radius,
md.contactname,
md.contactphone,
md.description dropshipdesc,
md.merchantid,
mm.merchantname,
mm.description merchantdesc,
mm.logoimageurl,
mm.coverimageurl,
dk.categorymenuid,
toph.openpodate,
toph.closepodate,
toph.statustransaksi
FROM transopenpodetaildropship t
inner join transopenpoheader toph on t.openpoheaderid = toph.openpoheaderid
inner join masterdropship md on t.dropshipid = md.dropshipid
inner join mastermerchant mm on md.merchantid = mm.merchantid
inner join transopenpodetaildropshipkategori dk on t.openpodetaildropshipid = dk.openpodetaildropshipid;

END;
$$;


ALTER FUNCTION public.transopenpodetaildropship_getalldata() OWNER TO postgres;

--
-- Name: transopenpodetaildropship_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transopenpodetaildropship_getdatabydynamicfilters(filters character varying) RETURNS TABLE(openpodetaildropshipid integer, openpoheaderid integer, starttime character varying, endtime character varying, tolerance character varying, keterangan character varying, ongkoskirim numeric, iscod boolean, dropshipid integer, dropshipname character varying, dropshipaddress character varying, latitude character varying, longitude character varying, radius numeric, contactname character varying, contactphone character varying, dropshipdesc character varying, merchantid integer, merchantname character varying, merchantdesc character varying, logoimageurl character varying, coverimageurl character varying, categorymenuid integer, openpodate timestamp without time zone, closepodate timestamp without time zone, statustransaksi character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from transopenpodetaildropship_GetAllData();
ELSE
    sql = 'SELECT 
t.openpodetaildropshipid,
t.openpoheaderid,
t.starttime,
t.endtime,
t.tolerance,
t.keterangan,
t.ongkoskirim,
t.iscod,
t.dropshipid,
md."label" dropshipname,
md.dropshipaddress,
md.latitude,
md.longitude,
md.radius,
md.contactname,
md.contactphone,
md.description dropshipdesc,
md.merchantid,
mm.merchantname,
mm.description merchantdesc,
mm.logoimageurl,
mm.coverimageurl,
topddk.categorymenuid,
toph.openpodate,
toph.closepodate,
toph.statustransaksi
FROM transopenpodetaildropship t
inner join transopenpoheader toph on t.openpoheaderid = toph.openpoheaderid
inner join masterdropship md on t.dropshipid = md.dropshipid
inner join mastermerchant mm on md.merchantid = mm.merchantid
inner join transopenpodetaildropshipkategori topddk on t.openpodetaildropshipid = topddk.openpodetaildropshipid

 WHERE ' || filters || '
 Order By openpodetaildropshipid';
 
 return query EXECUTE sql; 
 
 END IF;

END;
$$;


ALTER FUNCTION public.transopenpodetaildropship_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: transopenpodetaildropship_getexistingdropship(integer, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transopenpodetaildropship_getexistingdropship(p_dropshipid integer, p_openpodate character varying) RETURNS TABLE(openpoheaderid integer, openpodate timestamp without time zone, closepodate timestamp without time zone, statustransaksi character varying, waktuentry timestamp without time zone, dropshipid integer, starttime character varying, endtime character varying)
    LANGUAGE plpgsql
    AS $$

begin
	return query
select t.openpoheaderid,
t.openpodate,
t.closepodate,
t.statustransaksi,
t.waktuentry,
topdd.dropshipid,
topdd.starttime,
topdd.endtime 
from transopenpoheader t 
inner join transopenpodetaildropship topdd on t.openpoheaderid = topdd.openpoheaderid
where topdd.dropshipid = p_dropshipid and date(t.openpodate) >= p_openpodate::date
and lower(t.statustransaksi) = 'open';

END;
$$;


ALTER FUNCTION public.transopenpodetaildropship_getexistingdropship(p_dropshipid integer, p_openpodate character varying) OWNER TO postgres;

--
-- Name: transopenpodetaildropship_insertdata(integer, integer, character varying, character varying, character varying, character varying, numeric, boolean); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transopenpodetaildropship_insertdata(p_openpoheaderid integer, p_dropshipid integer, p_starttime character varying, p_endtime character varying, p_tolerance character varying, p_keterangan character varying, p_ongkoskirim numeric, p_iscod boolean) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

INSERT INTO transopenpodetaildropship 
(
openpoheaderid,
dropshipid,
starttime,
endtime,
tolerance,
keterangan,
ongkoskirim,
iscod
) 
 
 VALUES(
p_openpoheaderid,
p_dropshipid,
p_starttime,
p_endtime,
p_tolerance,
p_keterangan,
p_ongkoskirim,
p_iscod
) RETURNING openpodetaildropshipid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.transopenpodetaildropship_insertdata(p_openpoheaderid integer, p_dropshipid integer, p_starttime character varying, p_endtime character varying, p_tolerance character varying, p_keterangan character varying, p_ongkoskirim numeric, p_iscod boolean) OWNER TO postgres;

--
-- Name: transopenpodetaildropship_updatedata(integer, integer, character varying, character varying, character varying, character varying, numeric, boolean); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transopenpodetaildropship_updatedata(p_openpoheaderid integer, p_dropshipid integer, p_starttime character varying, p_endtime character varying, p_tolerance character varying, p_keterangan character varying, p_ongkoskirim numeric, p_iscod boolean) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

UPDATE transopenpodetaildropship set 
openpoheaderid=p_openpoheaderid,
dropshipid=p_dropshipid,
starttime=p_starttime,
endtime=p_endtime,
tolerance=p_tolerance,
keterangan=p_keterangan,
ongkoskirim=p_ongkoskirim,
iscod=p_iscod
 
 WHERE openpodetaildropshipid=p_openpodetaildropshipid
RETURNING openpodetaildropshipid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.transopenpodetaildropship_updatedata(p_openpoheaderid integer, p_dropshipid integer, p_starttime character varying, p_endtime character varying, p_tolerance character varying, p_keterangan character varying, p_ongkoskirim numeric, p_iscod boolean) OWNER TO postgres;

--
-- Name: transopenpodetaildropshipkategori_insertdata(integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transopenpodetaildropshipkategori_insertdata(p_openpodetaildropshipid integer, p_categorymenuid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

INSERT INTO transopenpodetaildropshipkategori 
(
openpodetaildropshipid,
categorymenuid
) 
 
 VALUES(
p_openpodetaildropshipid,
p_categorymenuid
) RETURNING 1 INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.transopenpodetaildropshipkategori_insertdata(p_openpodetaildropshipid integer, p_categorymenuid integer) OWNER TO postgres;

--
-- Name: transopenpodetailproduct_deletedata(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transopenpodetailproduct_deletedata(p_openpodetailproductid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

DELETE FROM transopenpodetailproduct 
 
 WHERE openpodetailproductid=p_openpodetailproductid
RETURNING openpodetailproductid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.transopenpodetailproduct_deletedata(p_openpodetailproductid integer) OWNER TO postgres;

--
-- Name: transopenpodetailproduct_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transopenpodetailproduct_getalldata() RETURNS TABLE(openpodetailproductid integer, openpodetaildropshipid integer, openpoheaderid integer, productid integer, productname character varying, productimageurl character varying, productprice numeric, kuotamin numeric, kuotamax numeric, qtymin numeric, qtymax numeric, kuota numeric, categorymenuid integer, isactive boolean, isbutton boolean, qtyorder numeric, statustransaksi character varying)
    LANGUAGE plpgsql
    AS $$
begin


return query 
  SELECT 
t.openpodetailproductid,
t.openpodetaildropshipid,
t.openpoheaderid,
t.productid,
mp.productname,
mp.productimageurl,
t.productprice,
t.kuotamin,
t.kuotamax,
t.qtymin,
t.qtymax, 
mp.kuota,
mp.categorymenuid,
mp.isactive,
mp.isbutton,
coalesce(tod.qtyorder,0) qtyorder,
toph.statustransaksi
FROM transopenpodetailproduct t 
inner join transopenpoheader toph on t.openpoheaderid = toph.openpoheaderid
inner join masterproduct mp on t.productid = mp.productid
left join (
select toh.openpoheaderid,toh.openpodetaildropshipid,tod.openpodetailproductid,
	sum(tod.qty) qtyorder
	from transorderdetail tod
	inner join transorderheader toh on tod.orderheaderid = toh.orderheaderid
	group by toh.openpoheaderid,toh.openpodetaildropshipid,tod.openpodetailproductid
) tod on t.openpoheaderid = tod.openpoheaderid 
and t.openpodetaildropshipid = tod.openpodetaildropshipid
and t.openpodetailproductid = tod.openpodetailproductid;

END;
$$;


ALTER FUNCTION public.transopenpodetailproduct_getalldata() OWNER TO postgres;

--
-- Name: transopenpodetailproduct_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transopenpodetailproduct_getdatabydynamicfilters(filters character varying) RETURNS TABLE(openpodetailproductid integer, openpodetaildropshipid integer, openpoheaderid integer, productid integer, productname character varying, productimageurl character varying, productprice numeric, kuotamin numeric, kuotamax numeric, qtymin numeric, qtymax numeric, kuota numeric, categorymenuid integer, isactive boolean, isbutton boolean, qtyorder numeric, statustransaksi character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from transopenpodetailproduct_GetAllData();
else
--wajib memfilter by userid untuk membedakan get po detail product ditampilkan untuk user siapa 
--(karena ada favorit product jadi harus spesifik ke user, jika bukan ditampilkan untuk user maka userid = 0)

    sql = ' SELECT 
t.openpodetailproductid,
t.openpodetaildropshipid,
t.openpoheaderid,
t.productid,
mp.productname,
mp.productimageurl,
t.productprice,
t.kuotamin,
t.kuotamax,
t.qtymin,
t.qtymax, 
mp.kuota,
mp.categorymenuid,
mp.isactive,
mp.isbutton,
coalesce(tod.qtyorder,0) qtyorder,
toph.statustransaksi
FROM transopenpodetailproduct t 
inner join transopenpoheader toph on t.openpoheaderid = toph.openpoheaderid
inner join masterproduct mp on t.productid = mp.productid
left join (
select toh.openpoheaderid,toh.openpodetaildropshipid,tod.openpodetailproductid,
	sum(tod.qty) qtyorder
	from transorderdetail tod
	inner join transorderheader toh on tod.orderheaderid = toh.orderheaderid
	group by toh.openpoheaderid,toh.openpodetaildropshipid,tod.openpodetailproductid
) tod on t.openpoheaderid = tod.openpoheaderid 
and t.openpodetaildropshipid = tod.openpodetaildropshipid
and t.openpodetailproductid = tod.openpodetailproductid
 WHERE ' || filters || '
 Order By t.openpodetailproductid';
 
 return query EXECUTE sql; 
 
 END IF;

END;
$$;


ALTER FUNCTION public.transopenpodetailproduct_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: transopenpodetailproduct_insertdata(integer, integer, integer, numeric, numeric, numeric, numeric, numeric); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transopenpodetailproduct_insertdata(p_openpodetaildropshipid integer, p_openpoheaderid integer, p_productid integer, p_productprice numeric, p_kuotamin numeric, p_kuotamax numeric, p_qtymax numeric, p_qtymin numeric) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

INSERT INTO transopenpodetailproduct 
(
openpodetaildropshipid,
openpoheaderid,
productid,
productprice,
kuotamin,
kuotamax,
qtymax,
qtymin
) 
 
 VALUES(
p_openpodetaildropshipid,
p_openpoheaderid,
p_productid,
p_productprice,
p_kuotamin,
p_kuotamax,
p_qtymax,
p_qtymin
) RETURNING openpodetailproductid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.transopenpodetailproduct_insertdata(p_openpodetaildropshipid integer, p_openpoheaderid integer, p_productid integer, p_productprice numeric, p_kuotamin numeric, p_kuotamax numeric, p_qtymax numeric, p_qtymin numeric) OWNER TO postgres;

--
-- Name: transopenpodetailproduct_updatedata(integer, integer, integer, integer, numeric, numeric, numeric, numeric, numeric); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transopenpodetailproduct_updatedata(p_openpodetailproductid integer, p_openpodetaildropshipid integer, p_openpoheaderid integer, p_productid integer, p_productprice numeric, p_kuotamin numeric, p_kuotamax numeric, p_qtymax numeric, p_qtymin numeric) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

UPDATE transopenpodetailproduct set 
openpodetaildropshipid=p_openpodetaildropshipid,
openpoheaderid=p_openpoheaderid,
productid=p_productid,
productprice=p_productprice,
kuotamin=p_kuotamin,
kuotamax=p_kuotamax,
qtymax=p_qtymax,
qtymin=p_qtymin
 
 WHERE openpodetailproductid=p_openpodetailproductid
RETURNING openpodetailproductid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.transopenpodetailproduct_updatedata(p_openpodetailproductid integer, p_openpodetaildropshipid integer, p_openpoheaderid integer, p_productid integer, p_productprice numeric, p_kuotamin numeric, p_kuotamax numeric, p_qtymax numeric, p_qtymin numeric) OWNER TO postgres;

--
-- Name: transopenpoheader_deletedata(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transopenpoheader_deletedata(p_openpoheaderid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

DELETE FROM transopenpoheader 
 
 WHERE openpoheaderid=p_openpoheaderid 
RETURNING openpoheaderid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.transopenpoheader_deletedata(p_openpoheaderid integer) OWNER TO postgres;

--
-- Name: transopenpoheader_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transopenpoheader_getalldata() RETURNS TABLE(openpoheaderid integer, openpodate timestamp without time zone, closepodate timestamp without time zone, kota character varying, namakota character varying, statustransaksi character varying, waktuentry timestamp without time zone, merchantid integer, useridmerchant integer)
    LANGUAGE plpgsql
    AS $$
BEGIN

return query 
 SELECT 
t.openpoheaderid,
t.openpodate,
t.closepodate,
t.kota,
mk.nama as namakota,
t.statustransaksi,
t.waktuentry,
t.merchantid,
mm.userid useridmerchant
FROM transopenpoheader t
left join masterkota mk on t.kota = mk.id::varchar
inner join mastermerchant mm on t.merchantid = mm.merchantid;

	
END;
$$;


ALTER FUNCTION public.transopenpoheader_getalldata() OWNER TO postgres;

--
-- Name: transopenpoheader_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transopenpoheader_getdatabydynamicfilters(filters character varying) RETURNS TABLE(openpoheaderid integer, openpodate timestamp without time zone, closepodate timestamp without time zone, kota character varying, namakota character varying, statustransaksi character varying, waktuentry timestamp without time zone, merchantid integer, useridmerchant integer)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from transopenpoheader_GetAllData();
ELSE
    sql = 'SELECT 
t.openpoheaderid,
t.openpodate,
t.closepodate,
t.kota,
mk.nama as namakota,
t.statustransaksi,
t.waktuentry,
t.merchantid,
mm.userid useridmerchant
FROM transopenpoheader t
left join masterkota mk on t.kota = mk.id::varchar
inner join mastermerchant mm on t.merchantid = mm.merchantid
 WHERE ' || filters || '
 Order By openpoheaderid';
 
 return query EXECUTE sql; 
 
 END IF;

END;
$$;


ALTER FUNCTION public.transopenpoheader_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: transopenpoheader_insertdata(timestamp without time zone, timestamp without time zone, character varying, character varying, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transopenpoheader_insertdata(p_openpodate timestamp without time zone, p_closepodate timestamp without time zone, p_kota character varying, p_statustransaksi character varying, p_merchantid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

INSERT INTO transopenpoheader 
(
	openpodate,
	closepodate,
	kota,
	statustransaksi,
	waktuentry,
	merchantid
) 
 
 VALUES(
	p_openpodate,
	p_closepodate,
	p_kota,
	p_statustransaksi,
	current_timestamp,
	p_merchantid
) RETURNING openpoheaderid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.transopenpoheader_insertdata(p_openpodate timestamp without time zone, p_closepodate timestamp without time zone, p_kota character varying, p_statustransaksi character varying, p_merchantid integer) OWNER TO postgres;

--
-- Name: transopenpoheader_updatedata(integer, timestamp without time zone, timestamp without time zone, character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transopenpoheader_updatedata(p_openpoheaderid integer, p_openpodate timestamp without time zone, p_closepodate timestamp without time zone, p_kota character varying, p_statustransaksi character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

UPDATE transopenpoheader set 
openpodate=p_openpodate,
closepodate=p_closepodate,
kota=p_kota,
statustransaksi=p_statustransaksi,
waktuentry = current_timestamp
 
 WHERE openpoheaderid=p_openpoheaderid 
RETURNING openpoheaderid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.transopenpoheader_updatedata(p_openpoheaderid integer, p_openpodate timestamp without time zone, p_closepodate timestamp without time zone, p_kota character varying, p_statustransaksi character varying) OWNER TO postgres;

--
-- Name: transorderdetail_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transorderdetail_getalldata() RETURNS TABLE(orderdetailid integer, orderheaderid integer, openpodetailproductid integer, qty numeric, notes character varying, productid integer, productname character varying, productsatuan character varying, productimageurl character varying, productdescription character varying, productprice numeric)
    LANGUAGE plpgsql
    AS $$
BEGIN

return query 
 select  
t.orderdetailid,
t.orderheaderid,
t.openpodetailproductid,
t.qty,
t.notes,
mp.productid,
mp.productname,
mp.productsatuan,
mp.productimageurl,
mp.productdescription,
topdp.productprice
FROM transorderdetail t 
inner join transopenpodetailproduct topdp on t.openpodetailproductid = topdp.openpodetailproductid
inner join masterproduct mp on topdp.productid = mp.productid;

END;
$$;


ALTER FUNCTION public.transorderdetail_getalldata() OWNER TO postgres;

--
-- Name: transorderdetail_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transorderdetail_getdatabydynamicfilters(filters character varying) RETURNS TABLE(orderdetailid integer, orderheaderid integer, openpodetailproductid integer, qty numeric, notes character varying, productid integer, productname character varying, productsatuan character varying, productimageurl character varying, productdescription character varying, productprice numeric)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from transorderdetail_GetAllData();
ELSE
    sql = 'select  
t.orderdetailid,
t.orderheaderid,
t.openpodetailproductid,
t.qty,
t.notes,
mp.productid,
mp.productname,
mp.productsatuan,
mp.productimageurl,
mp.productdescription,
topdp.productprice
FROM transorderdetail t 
inner join transopenpodetailproduct topdp on t.openpodetailproductid = topdp.openpodetailproductid
inner join masterproduct mp on topdp.productid = mp.productid
 WHERE ' || filters || '
 Order By orderdetailid';
 
 return query EXECUTE sql; 
 
 END IF;

END;
$$;


ALTER FUNCTION public.transorderdetail_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: transorderdetail_insertdata(integer, integer, numeric, numeric, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transorderdetail_insertdata(p_orderheaderid integer, p_openpodetailproductid integer, p_qty numeric, p_harga numeric, p_notes character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0;
 currqty decimal = 0;
 maxkuota decimal = 0;

BEGIN

maxkuota := (select coalesce(kuotamax,0) from transopenpodetailproduct where openpodetailproductid = p_openpodetailproductid);
currqty := (select coalesce(sum(qty),0) from transorderdetail where openpodetailproductid = p_openpodetailproductid);

if((currqty + p_qty) > maxkuota ) then
return query select -1;
else
INSERT INTO transorderdetail 
(orderheaderid,
openpodetailproductid,
qty,
 harga,
 subtotal,
notes
) 
 
 VALUES(
p_orderheaderid,
p_openpodetailproductid,
p_qty,
	 p_harga,
	 p_qty * p_harga,
p_notes
) RETURNING orderdetailid INTO lastid;
 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
end if;

END;
$$;


ALTER FUNCTION public.transorderdetail_insertdata(p_orderheaderid integer, p_openpodetailproductid integer, p_qty numeric, p_harga numeric, p_notes character varying) OWNER TO postgres;

--
-- Name: transorderheader_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transorderheader_getalldata() RETURNS TABLE(orderheaderid integer, openpoheaderid integer, openpodetaildropshipid integer, paymentmethodid integer, paymentmethod character varying, notransaksi character varying, userentry integer, avatarurl character varying, username text, email character varying, waktuentry timestamp without time zone, longitude character varying, latitude character varying, address character varying, total numeric, notesaddress character varying, statustransorderid integer, statustransaksi character varying, userclosed integer, usernameclosed text, statuspembayaranid integer, statuspembayaran character varying, merchantid integer, merchantname character varying, coverimageurl character varying, logoimageurl character varying, description character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN

return query 
   SELECT 
t.orderheaderid,
t.openpoheaderid,
t.openpodetaildropshipid,
t.paymentmethodid,
mpm.keterangan as paymentmethod,
t.notransaksi,
t.userentry,
u.avatarurl,
u.firstname || ' ' || u.lastname as username,
u.email,
t.waktuentry,
t.longitude,
t.latitude,
t.address,
t.total,
t.notesaddress,
t.statustransorderid,
msto.keterangan statustransaksi,
t.userclosed,
uc.firstname || ' ' || uc.lastname as usernameclosed,
msp.statuspembayaranid,
msp.keterangan as statuspembayaran,
mm.merchantid,
mm.merchantname,
mm.coverimageurl,
mm.logoimageurl,
mm.description 
FROM transorderheader t 
inner join transopenpoheader toph on t.openpoheaderid = toph.openpoheaderid 
inner join mastermerchant mm on toph.merchantid = mm.merchantid 
left join masterstatustransaksiorder msto on t.statustransorderid = msto.statustransorderid
left join masterpaymentmethod mpm on t.paymentmethodid = mpm.paymentmethodid
inner join users u on t.userentry = u.userid
left join users uc on t.userclosed = uc.userid
left join transpembayaran tp on t.orderheaderid = tp.orderheaderid 
left join masterstatuspembayaran msp on tp.statuspembayaranid = msp.statuspembayaranid
;

END;
$$;


ALTER FUNCTION public.transorderheader_getalldata() OWNER TO postgres;

--
-- Name: transorderheader_getalldatauserbelumbayarbyopenpoheaderid(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transorderheader_getalldatauserbelumbayarbyopenpoheaderid(p_openpoheaderid integer) RETURNS TABLE(orderheaderid integer, userid integer, merchantid integer)
    LANGUAGE plpgsql
    AS $$
BEGIN

return query 
 select t.orderheaderid,t.userentry as userid,toph.merchantid 
from transorderheader t 
inner join transopenpoheader toph on t.openpoheaderid = toph.openpoheaderid 
where t.orderheaderid  not in (select tp.orderheaderid from transpembayaran tp)
and toph.closepodate < now()
and toph.openpoheaderid = p_openpoheaderid;

    
END;
$$;


ALTER FUNCTION public.transorderheader_getalldatauserbelumbayarbyopenpoheaderid(p_openpoheaderid integer) OWNER TO postgres;

--
-- Name: transorderheader_getalldatausersudahbayarbyopenpoheaderid(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transorderheader_getalldatausersudahbayarbyopenpoheaderid(p_openpoheaderid integer) RETURNS TABLE(orderheaderid integer, userid integer, merchantid integer)
    LANGUAGE plpgsql
    AS $$
BEGIN

return query 
 select t.orderheaderid,t.userentry as userid,toph.merchantid 
from transorderheader t 
inner join transopenpoheader toph on t.openpoheaderid = toph.openpoheaderid 
where t.orderheaderid in (select tp.orderheaderid from transpembayaran tp)
and toph.closepodate < now()
and toph.openpoheaderid = p_openpoheaderid;

    
END;
$$;


ALTER FUNCTION public.transorderheader_getalldatausersudahbayarbyopenpoheaderid(p_openpoheaderid integer) OWNER TO postgres;

--
-- Name: transorderheader_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transorderheader_getdatabydynamicfilters(filters character varying) RETURNS TABLE(orderheaderid integer, openpoheaderid integer, openpodetaildropshipid integer, paymentmethodid integer, paymentmethod character varying, notransaksi character varying, userentry integer, avatarurl character varying, username text, email character varying, waktuentry timestamp without time zone, longitude character varying, latitude character varying, address character varying, total numeric, notesaddress character varying, statustransorderid integer, statustransaksi character varying, userclosed integer, usernameclosed text, statuspembayaranid integer, statuspembayaran character varying, merchantid integer, merchantname character varying, coverimageurl character varying, logoimageurl character varying, description character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from transorderheader_GetAllData();
ELSE
    sql = 'SELECT 
t.orderheaderid,
t.openpoheaderid,
t.openpodetaildropshipid,
t.paymentmethodid,
mpm.keterangan as paymentmethod,
t.notransaksi,
t.userentry,
u.avatarurl,
u.firstname || '' '' || u.lastname as username,
u.email,
t.waktuentry,
t.longitude,
t.latitude,
t.address,
t.total,
t.notesaddress,
t.statustransorderid,
msto.keterangan statustransaksi,
t.userclosed,
uc.firstname || '' '' || uc.lastname as usernameclosed,
msp.statuspembayaranid,
msp.keterangan as statuspembayaran,
mm.merchantid,
mm.merchantname,
mm.coverimageurl,
mm.logoimageurl,
mm.description 
FROM transorderheader t 
inner join transopenpoheader toph on t.openpoheaderid = toph.openpoheaderid 
inner join mastermerchant mm on toph.merchantid = mm.merchantid 
left join masterstatustransaksiorder msto on t.statustransorderid = msto.statustransorderid
left join masterpaymentmethod mpm on t.paymentmethodid = mpm.paymentmethodid
inner join users u on t.userentry = u.userid
left join users uc on t.userclosed = uc.userid
left join transpembayaran tp on t.orderheaderid = tp.orderheaderid 
left join masterstatuspembayaran msp on tp.statuspembayaranid = msp.statuspembayaranid
 WHERE ' || filters || '
 Order By orderheaderid';
 
 return query EXECUTE sql; 
 
 END IF;

END;
$$;


ALTER FUNCTION public.transorderheader_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: transorderheader_insertdata(integer, integer, integer, integer, character varying, character varying, character varying, numeric, character varying, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transorderheader_insertdata(p_openpoheaderid integer, p_openpodetaildropshipid integer, p_paymentmethodid integer, p_userentry integer, p_longitude character varying, p_latitude character varying, p_address character varying, p_total numeric, p_notesaddress character varying, p_statustransorderid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 
 notrans varchar = '';
 pocloseddate timestamp;

BEGIN

notrans := (select * from mastercounter_INSERTUPDATE('ORD','TRANSAKSI ORDER'));
pocloseddate := (select closepodate from transopenpoheader where openpoheaderid = p_openpoheaderid limit 1);

if(notrans <> '') then
if(current_timestamp > pocloseddate) then
return query select -1; 
else
INSERT INTO transorderheader 
(
openpoheaderid,
openpodetaildropshipid,
paymentmethodid,
notransaksi,
userentry,
waktuentry,
longitude,
latitude,
address,
total,
notesaddress,
	StatusTransOrderId
) 
 
 VALUES(
p_openpoheaderid,
p_openpodetaildropshipid,
p_paymentmethodid,
notrans,
p_userentry,
CURRENT_TIMESTAMP,
p_longitude,
p_latitude,
p_address,
p_total,
p_notesaddress,
	 p_statustransorderid
) RETURNING orderheaderid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;
end if;
end if;

return query select lastid;

END;
$$;


ALTER FUNCTION public.transorderheader_insertdata(p_openpoheaderid integer, p_openpodetaildropshipid integer, p_paymentmethodid integer, p_userentry integer, p_longitude character varying, p_latitude character varying, p_address character varying, p_total numeric, p_notesaddress character varying, p_statustransorderid integer) OWNER TO postgres;

--
-- Name: transorderheader_updatestatustransaksi(integer, integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transorderheader_updatestatustransaksi(p_orderheaderid integer, p_statustransorderid integer, p_userclosed integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

UPDATE transorderheader set 
statustransorderid=p_statustransorderid,
userclosed = p_userclosed
 
 WHERE orderheaderid=p_orderheaderid 
RETURNING orderheaderid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.transorderheader_updatestatustransaksi(p_orderheaderid integer, p_statustransorderid integer, p_userclosed integer) OWNER TO postgres;

--
-- Name: transpembayaran_deletedata(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transpembayaran_deletedata(p_orderheaderid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

DELETE FROM transpembayaran 
 
 WHERE orderheaderid=p_orderheaderid 
RETURNING pembayaranid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.transpembayaran_deletedata(p_orderheaderid integer) OWNER TO postgres;

--
-- Name: transpembayaran_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transpembayaran_getalldata() RETURNS TABLE(pembayaranid integer, orderheaderid integer, jumlahbayar numeric, digitbayar integer, idrekening integer, noinvoice character varying, noref character varying, buktibayarurl character varying, statuspembayaranid integer, waktubayar timestamp without time zone, waktuentry timestamp without time zone)
    LANGUAGE plpgsql
    AS $$

BEGIN

return query 
 SELECT 
t.pembayaranid,
t.orderheaderid,
t.jumlahbayar,
t.digitbayar,
t.idrekening,
t.noinvoice,
t.noref,
t.buktibayarurl,
t.statuspembayaranid,
t.waktubayar,
t.waktuentry
FROM transpembayaran t;



    
END;
$$;


ALTER FUNCTION public.transpembayaran_getalldata() OWNER TO postgres;

--
-- Name: transpembayaran_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transpembayaran_getdatabydynamicfilters(filters character varying) RETURNS TABLE(pembayaranid integer, orderheaderid integer, jumlahbayar numeric, digitbayar integer, idrekening integer, noinvoice character varying, noref character varying, buktibayarurl character varying, statuspembayaranid integer, waktubayar timestamp without time zone, waktuentry timestamp without time zone)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from transpembayaran_GetAllData();
ELSE
    sql = 'SELECT 
t.pembayaranid,
t.orderheaderid,
t.jumlahbayar,
t.digitbayar,
t.idrekening,
t.noinvoice,
t.noref,
t.buktibayarurl,
t.statuspembayaranid,
t.waktubayar,
t.waktuentry
FROM transpembayaran t
 WHERE ' || filters || '
 Order By pembayaranid';
 
 return query EXECUTE sql; 
 
 END IF;



    
END;
$$;


ALTER FUNCTION public.transpembayaran_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: transpembayaran_insertdata(integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transpembayaran_insertdata(p_orderheaderid integer, p_idrekening integer) RETURNS TABLE(orderheaderid bigint, jumlahbayar numeric, rekening character varying, waktuentry timestamp without time zone, closepodate timestamp without time zone, status integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 
 invoice varchar = '';
 closepodate timestamp;
 waktu timestamp = now();
 jumlah decimal = 0;
 digit int = 0;
rekening varchar = '';
begin
	jumlah = (select total from transorderheader toh inner join users u on toh.userentry = u.userid where toh.orderheaderid = p_orderheaderid limit 1);
	--digit = (select cast(right(u.phone,3) as int) from transorderheader toh inner join users u on toh.userentry = u.userid where toh.orderheaderid = p_orderheaderid limit 1);
	digit = (
		SELECT  kode
		FROM     GENERATE_SERIES (100, 999) AS kode
		where kode not in (select digitbayar from transpembayaran t where statuspembayaranid in (1,2)) 
		--generate digit bayar tidak boleh sama dengan digit bayar pembayaran yg masih aktif
		order by random()
		LIMIT  1
	);
	invoice := (select *from mastercounter_insertupdate('INV', 'INVOICE PEMBAYARAN'));
	closepodate := (select toph.closepodate from transorderheader t 
inner join transopenpoheader toph on t.openpoheaderid = toph.openpoheaderid 
where t.orderheaderid = p_orderheaderid 
limit 1);

rekening := (select norekening from masterrekening where idrekening = p_idrekening limit 1);


if(invoice <> '') then
if(current_timestamp > closepodate) then
lastid := -1; --jika melebihi tanggal selesai po
else 
INSERT INTO transpembayaran 
(
orderheaderid,
jumlahbayar,
digitbayar,
idrekening,
noinvoice,
statuspembayaranid,
waktuentry
) 
 
 VALUES(
p_orderheaderid,
jumlah,
digit,
p_idrekening,
invoice,
1,
waktu
) 
RETURNING pembayaranid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;
end if;
else 
	lastid := -2; --gagal membuat nomor invoice
end if;

return query select cast(p_orderheaderid as bigint) as orderheaderid,cast((jumlah + digit) as numeric) as jumlahbayar,rekening as rekening ,waktu as waktu,closepodate as closepodate,lastid as status;
    
END;
$$;


ALTER FUNCTION public.transpembayaran_insertdata(p_orderheaderid integer, p_idrekening integer) OWNER TO postgres;

--
-- Name: transpembayaran_updatepembayaran(integer, integer, character varying, character varying, integer, timestamp without time zone); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transpembayaran_updatepembayaran(p_pembayaranid integer, p_orderheaderid integer, p_noref character varying, p_buktibayarurl character varying, p_statuspembayaranid integer, p_waktubayar timestamp without time zone) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 
 closepodate timestamp;
begin
	
closepodate := (select toph.closepodate from transorderheader t 
inner join transopenpoheader toph on t.openpoheaderid = toph.openpoheaderid 
where t.orderheaderid = p_orderheaderid 
limit 1);

if(p_waktubayar > closepodate) then
lastid = -1; -- jika melebihi tgl close po
else
UPDATE transpembayaran set 
noref=p_noref,
buktibayarurl=p_buktibayarurl,
statuspembayaranid=p_statuspembayaranid,
waktubayar=p_waktubayar
 
 WHERE pembayaranid=p_pembayaranid 
RETURNING pembayaranid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;
end if;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.transpembayaran_updatepembayaran(p_pembayaranid integer, p_orderheaderid integer, p_noref character varying, p_buktibayarurl character varying, p_statuspembayaranid integer, p_waktubayar timestamp without time zone) OWNER TO postgres;

--
-- Name: transpembayaran_verifikasipembayaran(integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transpembayaran_verifikasipembayaran(p_pembayaranid integer, p_statuspembayaranid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

UPDATE transpembayaran set 
statuspembayaranid=p_statuspembayaranid
 
 WHERE pembayaranid=p_pembayaranid 
RETURNING pembayaranid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.transpembayaran_verifikasipembayaran(p_pembayaranid integer, p_statuspembayaranid integer) OWNER TO postgres;

--
-- Name: transpengiriman_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transpengiriman_getalldata() RETURNS TABLE(pengirimanid integer, orderheaderid integer, shipmentid integer, foto character varying, waktukirim timestamp without time zone, waktuterima timestamp without time zone, namapenerima character varying, fotobuktiterima character varying, keterangan character varying)
    LANGUAGE plpgsql
    AS $$

BEGIN

return query 
 SELECT 
t.pengirimanid,
t.orderheaderid,
t.shipmentid,
t.foto,
t.waktukirim,
t.waktuterima,
t.namapenerima,
t.fotobuktiterima,
t.keterangan
FROM transpengiriman t;

END;
$$;


ALTER FUNCTION public.transpengiriman_getalldata() OWNER TO postgres;

--
-- Name: transpengiriman_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transpengiriman_getdatabydynamicfilters(filters character varying) RETURNS TABLE(pengirimanid integer, orderheaderid integer, shipmentid integer, foto character varying, waktukirim timestamp without time zone, waktuterima timestamp without time zone, namapenerima character varying, fotobuktiterima character varying, keterangan character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from transpengiriman_GetAllData();
ELSE
    sql = 'SELECT 
t.pengirimanid,
t.orderheaderid,
t.shipmentid,
t.foto,
t.waktukirim,
t.waktuterima,
t.namapenerima,
t.fotobuktiterima,
t.keterangan
FROM transpengiriman t
 WHERE ' || filters || '
 Order By pengirimanid';
 
 return query EXECUTE sql; 
 
 END IF;
    
END;
$$;


ALTER FUNCTION public.transpengiriman_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: transpengiriman_insertdata(integer, integer, character varying, timestamp without time zone); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transpengiriman_insertdata(p_orderheaderid integer, p_shipmentid integer, p_foto character varying, p_waktukirim timestamp without time zone) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

INSERT INTO transpengiriman 
(
orderheaderid,
shipmentid,
foto,
waktukirim
) 
 
 VALUES(
p_orderheaderid,
p_shipmentid,
p_foto,
p_waktukirim
) RETURNING pengirimanid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.transpengiriman_insertdata(p_orderheaderid integer, p_shipmentid integer, p_foto character varying, p_waktukirim timestamp without time zone) OWNER TO postgres;

--
-- Name: transpengiriman_updatepenerima(integer, character varying, character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.transpengiriman_updatepenerima(p_orderheaderid integer, p_namapenerima character varying, p_fotobuktiterima character varying, p_keterangan character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

UPDATE transpengiriman set 
waktuterima=current_timestamp,
namapenerima=p_namapenerima,
fotobuktiterima=p_fotobuktiterima,
keterangan=p_keterangan
 WHERE orderheaderid=p_orderheaderid
RETURNING orderheaderid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.transpengiriman_updatepenerima(p_orderheaderid integer, p_namapenerima character varying, p_fotobuktiterima character varying, p_keterangan character varying) OWNER TO postgres;

--
-- Name: users_deletedata(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.users_deletedata(p_userid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int; 

BEGIN

DELETE FROM users 
 WHERE userid=p_userid;
 GET DIAGNOSTICS rcount = ROW_COUNT; 

if(rcount > 0) then
    return query select 1;
else
    return query select 0;
end if;

    
END;
$$;


ALTER FUNCTION public.users_deletedata(p_userid integer) OWNER TO postgres;

--
-- Name: users_getalldata(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.users_getalldata() RETURNS TABLE(userid integer, email character varying, password character varying, firstname character varying, lastname character varying, birthdate date, region character varying, gender character varying, address character varying, postalcode character varying, cityid integer, phone character varying, job character varying, avatarurl character varying, identitycardurl character varying, createddate timestamp without time zone, lastlogin timestamp without time zone, lastlogout timestamp without time zone, isverified boolean, ismerchant boolean, isverifiedmerchant boolean, biometric character varying, islogin boolean, merchantid integer, merchantname character varying, description character varying, logoimageurl character varying, coverimageurl character varying, avgratingproduct numeric, avgratingpackaging numeric, avgratingdelivering numeric)
    LANGUAGE plpgsql
    AS $$
BEGIN

return query 
 SELECT 
u.userid,
u.email,
u.password,
u.firstname,
u.lastname,
u.birthdate,
u.region,
u.gender,
u.address,
u.postalcode,
u.cityid,
u.phone,
u.job,
u.avatarurl,
u.identitycardurl,
u.createddate,
u.lastlogin,
u.lastlogout,
u.isverified,
u.ismerchant,
u.isverifiedmerchant,
u.biometric,
u.islogin,
mm.merchantid,
mm.merchantname,
mm.description,
mm.logoimageurl,
mm.coverimageurl,
mm.avgratingproduct,
mm.avgratingpackaging,
mm.avgratingdelivering
FROM users u
left join mastermerchant mm on u.userid = mm.userid

order by u.userid;

    
END;
$$;


ALTER FUNCTION public.users_getalldata() OWNER TO postgres;

--
-- Name: users_getalldatabyemailandpassword(character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.users_getalldatabyemailandpassword(p_email character varying, p_password character varying) RETURNS TABLE(userid integer, email character varying, password character varying, firstname character varying, lastname character varying, birthdate date, region character varying, gender character varying, address character varying, postalcode character varying, cityid integer, phone character varying, job character varying, avatarurl character varying, identitycardurl character varying, createddate timestamp without time zone, lastlogin timestamp without time zone, lastlogout timestamp without time zone, isverified boolean, ismerchant boolean, biometric character varying, islogin boolean, merchantid integer, merchantname character varying, description character varying, logoimageurl character varying, coverimageurl character varying, avgratingproduct numeric, avgratingpackaging numeric, avgratingdelivering numeric)
    LANGUAGE plpgsql
    AS $$
BEGIN

return query 
 SELECT 
u.userid,
u.email,
u.password,
u.firstname,
u.lastname,
u.birthdate,
u.region,
u.gender,
u.address,
u.postalcode,
u.cityid,
u.phone,
u.job,
u.avatarurl,
u.identitycardurl,
u.createddate,
u.lastlogin,
u.lastlogout,
u.isverified,
u.ismerchant,
u.biometric,
u.islogin,
mm.merchantid,
mm.merchantname,
mm.description,
mm.logoimageurl,
mm.coverimageurl,
mm.avgratingproduct,
mm.avgratingpackaging,
mm.avgratingdelivering
FROM users u
left join mastermerchant mm on u.userid = mm.userid
where u.email = p_email and u.password = p_password;
    
END;
$$;


ALTER FUNCTION public.users_getalldatabyemailandpassword(p_email character varying, p_password character varying) OWNER TO postgres;

--
-- Name: users_getalldatabyphoneandpassword(character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.users_getalldatabyphoneandpassword(p_phone character varying, p_password character varying) RETURNS TABLE(userid integer, email character varying, password character varying, firstname character varying, lastname character varying, birthdate date, region character varying, gender character varying, address character varying, postalcode character varying, cityid integer, phone character varying, job character varying, avatarurl character varying, identitycardurl character varying, createddate timestamp without time zone, lastlogin timestamp without time zone, lastlogout timestamp without time zone, isverified boolean, ismerchant boolean, biometric character varying, islogin boolean, merchantid integer, merchantname character varying, description character varying, logoimageurl character varying, coverimageurl character varying, avgratingproduct numeric, avgratingpackaging numeric, avgratingdelivering numeric)
    LANGUAGE plpgsql
    AS $$
BEGIN

return query 
 SELECT 
u.userid,
u.email,
u.password,
u.firstname,
u.lastname,
u.birthdate,
u.region,
u.gender,
u.address,
u.postalcode,
u.cityid,
u.phone,
u.job,
u.avatarurl,
u.identitycardurl,
u.createddate,
u.lastlogin,
u.lastlogout,
u.isverified,
u.ismerchant,
u.biometric,
u.islogin,
mm.merchantid,
mm.merchantname,
mm.description,
mm.logoimageurl,
mm.coverimageurl,
mm.avgratingproduct,
mm.avgratingpackaging,
mm.avgratingdelivering
FROM users u
left join mastermerchant mm on u.userid = mm.userid
where u.phone = p_phone and u.password = p_password;
    
END;
$$;


ALTER FUNCTION public.users_getalldatabyphoneandpassword(p_phone character varying, p_password character varying) OWNER TO postgres;

--
-- Name: users_getalldatawithlimit(integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.users_getalldatawithlimit(p_limit integer, p_offset integer) RETURNS TABLE(userid integer, email character varying, password character varying, firstname character varying, lastname character varying, birthdate date, region character varying, gender character varying, address character varying, postalcode character varying, cityid integer, phone character varying, job character varying, avatarurl character varying, identitycardurl character varying, createddate timestamp without time zone, lastlogin timestamp without time zone, lastlogout timestamp without time zone, isverified boolean, ismerchant boolean, isverifiedmerchant boolean, biometric character varying, islogin boolean, merchantid integer, merchantname character varying, description character varying, logoimageurl character varying, coverimageurl character varying, avgratingproduct numeric, avgratingpackaging numeric, avgratingdelivering numeric)
    LANGUAGE plpgsql
    AS $$
BEGIN

return query 
 SELECT 
u.userid,
u.email,
u.password,
u.firstname,
u.lastname,
u.birthdate,
u.region,
u.gender,
u.address,
u.postalcode,
u.cityid,
u.phone,
u.job,
u.avatarurl,
u.identitycardurl,
u.createddate,
u.lastlogin,
u.lastlogout,
u.isverified,
u.ismerchant,
u.isverifiedmerchant,
u.biometric,
u.islogin,
mm.merchantid,
mm.merchantname,
mm.description,
mm.logoimageurl,
mm.coverimageurl,
mm.avgratingproduct,
mm.avgratingpackaging,
mm.avgratingdelivering
FROM users u
left join mastermerchant mm on u.userid = mm.userid
order by u.userid
limit p_limit offset p_offset;

    
END;
$$;


ALTER FUNCTION public.users_getalldatawithlimit(p_limit integer, p_offset integer) OWNER TO postgres;

--
-- Name: users_getdatabydynamicfilters(character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.users_getdatabydynamicfilters(filters character varying) RETURNS TABLE(userid integer, email character varying, password character varying, firstname character varying, lastname character varying, birthdate date, region character varying, gender character varying, address character varying, postalcode character varying, cityid integer, phone character varying, job character varying, avatarurl character varying, identitycardurl character varying, createddate timestamp without time zone, lastlogin timestamp without time zone, lastlogout timestamp without time zone, isverified boolean, ismerchant boolean, isverifiedmerchant boolean, biometric character varying, islogin boolean, merchantid integer, merchantname character varying, description character varying, logoimageurl character varying, coverimageurl character varying, avgratingproduct numeric, avgratingpackaging numeric, avgratingdelivering numeric)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
    return query select * from users_GetAllData();
ELSE
    sql = 'SELECT 
u.userid,
u.email,
u.password,
u.firstname,
u.lastname,
u.birthdate,
u.region,
u.gender,
u.address,
u.postalcode,
u.cityid,
u.phone,
u.job,
u.avatarurl,
u.identitycardurl,
u.createddate,
u.lastlogin,
u.lastlogout,
u.isverified,
u.ismerchant,
u.isverifiedmerchant,
u.biometric,
u.islogin,
mm.merchantid,
mm.merchantname,
mm.description,
mm.logoimageurl,
mm.coverimageurl,
mm.avgratingproduct,
mm.avgratingpackaging,
mm.avgratingdelivering
FROM users u
left join mastermerchant mm on u.userid = mm.userid

 WHERE ' || filters || '
 Order By u.userid';
 
 return query EXECUTE sql; 
 
 END IF;
 
END;
$$;


ALTER FUNCTION public.users_getdatabydynamicfilters(filters character varying) OWNER TO postgres;

--
-- Name: users_getdatabydynamicfilterswithlimit(character varying, integer, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.users_getdatabydynamicfilterswithlimit(filters character varying, p_limit integer, p_offset integer) RETURNS TABLE(userid integer, email character varying, password character varying, firstname character varying, lastname character varying, birthdate date, region character varying, gender character varying, address character varying, postalcode character varying, cityid integer, phone character varying, job character varying, avatarurl character varying, identitycardurl character varying, createddate timestamp without time zone, lastlogin timestamp without time zone, lastlogout timestamp without time zone, isverified boolean, ismerchant boolean, isverifiedmerchant boolean, biometric character varying, islogin boolean, merchantid integer, merchantname character varying, description character varying, logoimageurl character varying, coverimageurl character varying, avgratingproduct numeric, avgratingpackaging numeric, avgratingdelivering numeric)
    LANGUAGE plpgsql
    AS $$
DECLARE sql text; 
BEGIN

if filters='' then 
  if(p_limit <= 0 and p_offset <= 0) then
    return query select * from users_GetAllData();
  else 
    return query select * from users_getalldatawithlimit(p_limit,p_offset);
  end if;
else
sql = 'SELECT 
u.userid,
u.email,
u.password,
u.firstname,
u.lastname,
u.birthdate,
u.region,
u.gender,
u.address,
u.postalcode,
u.cityid,
u.phone,
u.job,
u.avatarurl,
u.identitycardurl,
u.createddate,
u.lastlogin,
u.lastlogout,
u.isverified,
u.ismerchant,
u.isverifiedmerchant,
u.biometric,
u.islogin,
mm.merchantid,
mm.merchantname,
mm.description,
mm.logoimageurl,
mm.coverimageurl,
mm.avgratingproduct,
mm.avgratingpackaging,
mm.avgratingdelivering
FROM users u
left join mastermerchant mm on u.userid = mm.userid

 WHERE ' || filters || '
 Order By u.userid ' || 
 (select case 
 when (p_limit <= 0 and p_offset <= 0) then ''
 else 
' limit ' || p_limit || ' offset ' || p_offset
end);
return query EXECUTE sql; 
 
 END IF;
 
END;
$$;


ALTER FUNCTION public.users_getdatabydynamicfilterswithlimit(filters character varying, p_limit integer, p_offset integer) OWNER TO postgres;

--
-- Name: users_insertdata(character varying, character varying, character varying, character varying, timestamp without time zone, character varying, character varying, character varying, character varying, integer, character varying, character varying, character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.users_insertdata(p_email character varying, p_password character varying, p_firstname character varying, p_lastname character varying, p_birthdate timestamp without time zone, p_region character varying, p_gender character varying, p_address character varying, p_postalcode character varying, p_cityid integer, p_phone character varying, p_job character varying, p_avatarurl character varying, p_biometric character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int; 

BEGIN

	if exists(select 1 from users where email = p_email) then 
	return query select -1;
	else
INSERT INTO users 
(
email,
password,
firstname,
lastname,
birthdate,
region,
gender,
address,
postalcode,
cityid,
phone,
job,
avatarurl,
identitycardurl,
createddate,
isverified,
ismerchant,
isverifiedmerchant,
biometric
) 
 
 VALUES(
p_email,
p_password,
p_firstname,
p_lastname,
p_birthdate,
p_region,
p_gender,
p_address,
p_postalcode,
p_cityid,
p_phone,
p_job,
p_avatarurl,
null,
current_timestamp,
false,
false,
false,
p_biometric
);

 GET DIAGNOSTICS rcount = ROW_COUNT; 

if(rcount > 0) then
    return query select 1;
else
    return query select 0;
end if;
end if;

    
END;
$$;


ALTER FUNCTION public.users_insertdata(p_email character varying, p_password character varying, p_firstname character varying, p_lastname character varying, p_birthdate timestamp without time zone, p_region character varying, p_gender character varying, p_address character varying, p_postalcode character varying, p_cityid integer, p_phone character varying, p_job character varying, p_avatarurl character varying, p_biometric character varying) OWNER TO postgres;

--
-- Name: users_updatebiometric(integer, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.users_updatebiometric(p_userid integer, p_biometric character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int; 

BEGIN

UPDATE users set 
biometric=p_biometric
 WHERE userid=p_userid;
 GET DIAGNOSTICS rcount = ROW_COUNT; 

if(rcount > 0) then
    return query select 1;
else
    return query select 0;
end if;

    
END;
$$;


ALTER FUNCTION public.users_updatebiometric(p_userid integer, p_biometric character varying) OWNER TO postgres;

--
-- Name: users_updatedata(integer, character varying, character varying, character varying, character varying, timestamp without time zone, character varying, character varying, character varying, character varying, integer, character varying, character varying, character varying, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.users_updatedata(p_userid integer, p_email character varying, p_password character varying, p_firstname character varying, p_lastname character varying, p_birthdate timestamp without time zone, p_region character varying, p_gender character varying, p_address character varying, p_postalcode character varying, p_cityid integer, p_phone character varying, p_job character varying, p_avatarurl character varying, p_identitycardurl character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int; 

BEGIN

UPDATE users set 
email=p_email,
password=p_password,
firstname=p_firstname,
lastname=p_lastname,
birthdate=p_birthdate,
region=p_region,
gender=p_gender,
address=p_address,
postalcode=p_postalcode,
cityid=p_cityid,
phone=p_phone,
job=p_job,
avatarurl=p_avatarurl,
identitycardurl=p_identitycardurl
--isverified=p_isverified,
--ismerchant=p_ismerchant,
--isverifiedmerchant=p_isverifiedmerchant,
--biometric=p_biometric

 WHERE userid=p_userid;

 GET DIAGNOSTICS rcount = ROW_COUNT; 

if(rcount > 0) then
    return query select p_userid;
else
    return query select 0;
end if;

    
END;
$$;


ALTER FUNCTION public.users_updatedata(p_userid integer, p_email character varying, p_password character varying, p_firstname character varying, p_lastname character varying, p_birthdate timestamp without time zone, p_region character varying, p_gender character varying, p_address character varying, p_postalcode character varying, p_cityid integer, p_phone character varying, p_job character varying, p_avatarurl character varying, p_identitycardurl character varying) OWNER TO postgres;

--
-- Name: users_updatektp(integer, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.users_updatektp(p_userid integer, p_identitycardurl character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

UPDATE users set identitycardurl = p_identitycardurl
 
 WHERE userid = p_userid 
RETURNING userid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.users_updatektp(p_userid integer, p_identitycardurl character varying) OWNER TO postgres;

--
-- Name: users_updatepunishmentcount(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.users_updatepunishmentcount(p_userid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

UPDATE users set 
punishmentcount=(select count(1) from punishment where userid = p_userid limit 1)
 
 WHERE userid=p_userid 
RETURNING userid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.users_updatepunishmentcount(p_userid integer) OWNER TO postgres;

--
-- Name: users_updatestatuslogin(integer, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.users_updatestatuslogin(p_userid integer, p_status character varying) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare rcount int;
begin
	
rcount = 0;

	if exists( select 1 from users WHERE userid=p_userid limit 1) 
	then
	
	if(p_status = 'login') then
	update users set lastlogin = current_timestamp where userid = p_userid;
	 GET DIAGNOSTICS rcount = ROW_COUNT;
	
	else
	
	update users set lastlogout = current_timestamp where userid = p_userid;
	 GET DIAGNOSTICS rcount = ROW_COUNT;
	end if;
	end if;
	
	if(rcount > 0) then
		return query select 1;
	else
		return query select 0;
	end if;
	
	
end;
$$;


ALTER FUNCTION public.users_updatestatuslogin(p_userid integer, p_status character varying) OWNER TO postgres;

--
-- Name: users_updatestatusmerchant(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.users_updatestatusmerchant(p_userid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int := 0; 
BEGIN

if exists(select 1 from mastermerchant where userid = p_userid) then
UPDATE users set 
ismerchant=true
 WHERE userid=p_userid;
 GET DIAGNOSTICS rcount = ROW_COUNT; 
end if;

if(rcount > 0) then
    return query select 1;
else 
	if exists(select 1 from users u where userid = p_userid and ismerchant = 1) then
    return query select 1;
   else
   return query select 0;
  end if;
end if;

    
END;
$$;


ALTER FUNCTION public.users_updatestatusmerchant(p_userid integer) OWNER TO postgres;

--
-- Name: users_verifymerchant(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.users_verifymerchant(p_userid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

UPDATE users set isverifiedmerchant = true,ismerchant =true
 
 WHERE userid = p_userid and ismerchant = false
RETURNING userid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.users_verifymerchant(p_userid integer) OWNER TO postgres;

--
-- Name: users_verifyuser(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.users_verifyuser(p_userid integer) RETURNS TABLE(hasil integer)
    LANGUAGE plpgsql
    AS $$
declare 
 rcount int = 0; 
 lastid int = 0; 

BEGIN

UPDATE users set isverified = true
 
 WHERE userid = p_userid 
RETURNING userid INTO lastid;

 GET DIAGNOSTICS rcount = ROW_COUNT;

return query select lastid;
    
END;
$$;


ALTER FUNCTION public.users_verifyuser(p_userid integer) OWNER TO postgres;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- Name: bannermenu; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.bannermenu (
    bannermenuid integer NOT NULL,
    bannermenuname character varying(30) NOT NULL,
    bannerimageurl character varying NOT NULL,
    cityid integer
);


ALTER TABLE public.bannermenu OWNER TO postgres;

--
-- Name: bannermenu_bannermenuid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.bannermenu ALTER COLUMN bannermenuid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.bannermenu_bannermenuid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: favoritproduct; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.favoritproduct (
    productid integer NOT NULL,
    userid integer NOT NULL,
    waktuentry timestamp without time zone NOT NULL
);


ALTER TABLE public.favoritproduct OWNER TO postgres;

--
-- Name: mastercategorymenu; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.mastercategorymenu (
    categorymenuid integer NOT NULL,
    categorymenuname character varying(30) NOT NULL,
    categoryimageurl character varying NOT NULL
);


ALTER TABLE public.mastercategorymenu OWNER TO postgres;

--
-- Name: mastercategorymenu_categorymenuid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.mastercategorymenu ALTER COLUMN categorymenuid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.mastercategorymenu_categorymenuid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: mastercounter; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.mastercounter (
    id integer NOT NULL,
    prefix character varying(10) NOT NULL,
    jenistransaksi character varying NOT NULL,
    lastinc integer NOT NULL,
    lastid character varying NOT NULL
);


ALTER TABLE public.mastercounter OWNER TO postgres;

--
-- Name: mastercounter_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.mastercounter ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.mastercounter_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: masterdropship; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.masterdropship (
    dropshipid integer NOT NULL,
    merchantid integer NOT NULL,
    label character varying NOT NULL,
    longitude character varying(30) NOT NULL,
    latitude character varying(30) NOT NULL,
    dropshipaddress character varying NOT NULL,
    description character varying NOT NULL,
    contactname character varying(30) NOT NULL,
    contactphone character varying(20) NOT NULL,
    radius numeric NOT NULL,
    isactive boolean NOT NULL,
    ongkoskirim numeric,
    iscod boolean DEFAULT false NOT NULL
);


ALTER TABLE public.masterdropship OWNER TO postgres;

--
-- Name: masterdropship_dropshipid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.masterdropship ALTER COLUMN dropshipid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.masterdropship_dropshipid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: masterkota; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.masterkota (
    id integer NOT NULL,
    idprovinsi integer NOT NULL,
    nama character varying NOT NULL
);


ALTER TABLE public.masterkota OWNER TO postgres;

--
-- Name: mastermerchant; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.mastermerchant (
    merchantid integer NOT NULL,
    userid integer NOT NULL,
    merchantname character varying(30) NOT NULL,
    description character varying,
    logoimageurl character varying NOT NULL,
    coverimageurl character varying,
    avgratingproduct numeric DEFAULT 0,
    avgratingpackaging numeric DEFAULT 0,
    avgratingdelivering numeric DEFAULT 0,
    namabank character varying(30),
    nomorrekening character varying(50),
    namapemilikrekening character varying(50)
);


ALTER TABLE public.mastermerchant OWNER TO postgres;

--
-- Name: mastermerchant_merchantid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.mastermerchant ALTER COLUMN merchantid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.mastermerchant_merchantid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: masterpaymentmethod; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.masterpaymentmethod (
    paymentmethodid integer NOT NULL,
    keterangan character varying(20)
);


ALTER TABLE public.masterpaymentmethod OWNER TO postgres;

--
-- Name: masterpaymentmethod_paymentmethodid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.masterpaymentmethod ALTER COLUMN paymentmethodid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.masterpaymentmethod_paymentmethodid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: masterproduct; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.masterproduct (
    productid integer NOT NULL,
    merchantid integer NOT NULL,
    categorymenuid integer NOT NULL,
    productname character varying(60) NOT NULL,
    productimageurl character varying NOT NULL,
    productprice numeric(18,2) NOT NULL,
    productdescription character varying NOT NULL,
    isbutton boolean NOT NULL,
    isactive boolean NOT NULL,
    productsatuan character varying(15),
    kuota numeric,
    kuotamax numeric,
    kuotamin numeric,
    qtymax numeric,
    qtymin numeric,
    ishalal boolean NOT NULL
);


ALTER TABLE public.masterproduct OWNER TO postgres;

--
-- Name: masterproduct_productid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.masterproduct ALTER COLUMN productid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.masterproduct_productid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: masterprovinsi; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.masterprovinsi (
    id integer NOT NULL,
    nama character varying NOT NULL
);


ALTER TABLE public.masterprovinsi OWNER TO postgres;

--
-- Name: masterratingdelivering; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.masterratingdelivering (
    ratingdeliveringid integer NOT NULL,
    rating integer NOT NULL,
    userentry integer NOT NULL,
    waktuentry timestamp without time zone NOT NULL,
    merchantid integer NOT NULL
);


ALTER TABLE public.masterratingdelivering OWNER TO postgres;

--
-- Name: masterratingdelivering_ratingdeliveringid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.masterratingdelivering ALTER COLUMN ratingdeliveringid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.masterratingdelivering_ratingdeliveringid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: masterratingpackaging; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.masterratingpackaging (
    ratingpackagingid integer NOT NULL,
    rating integer NOT NULL,
    userentry integer NOT NULL,
    waktuentry timestamp without time zone NOT NULL,
    merchantid integer NOT NULL
);


ALTER TABLE public.masterratingpackaging OWNER TO postgres;

--
-- Name: masterratingpackaging_ratingpackagingid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.masterratingpackaging ALTER COLUMN ratingpackagingid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.masterratingpackaging_ratingpackagingid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: masterratingproduct; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.masterratingproduct (
    ratingproductid integer NOT NULL,
    rating integer NOT NULL,
    userentry integer NOT NULL,
    waktuentry timestamp without time zone NOT NULL,
    merchantid integer NOT NULL
);


ALTER TABLE public.masterratingproduct OWNER TO postgres;

--
-- Name: masterratingproduct_ratingproductid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.masterratingproduct ALTER COLUMN ratingproductid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.masterratingproduct_ratingproductid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: masterrekening; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.masterrekening (
    idrekening integer NOT NULL,
    norekening character varying NOT NULL,
    namapemilikrekening character varying NOT NULL,
    namabank character varying
);


ALTER TABLE public.masterrekening OWNER TO postgres;

--
-- Name: masterrekening_idrekening_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.masterrekening ALTER COLUMN idrekening ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.masterrekening_idrekening_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: mastershipment; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.mastershipment (
    shipmentid integer NOT NULL,
    keterangan character varying(30) NOT NULL
);


ALTER TABLE public.mastershipment OWNER TO postgres;

--
-- Name: mastershipment_shipmentid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.mastershipment ALTER COLUMN shipmentid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.mastershipment_shipmentid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: masterstatuspembayaran; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.masterstatuspembayaran (
    statuspembayaranid integer NOT NULL,
    keterangan character varying
);


ALTER TABLE public.masterstatuspembayaran OWNER TO postgres;

--
-- Name: masterstatuspembayaran_statuspembayaranid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.masterstatuspembayaran ALTER COLUMN statuspembayaranid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.masterstatuspembayaran_statuspembayaranid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: masterstatustransaksiorder; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.masterstatustransaksiorder (
    statustransorderid integer NOT NULL,
    keterangan character varying(30) NOT NULL
);


ALTER TABLE public.masterstatustransaksiorder OWNER TO postgres;

--
-- Name: masterstatustransaksiorder_statustransorderid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.masterstatustransaksiorder ALTER COLUMN statustransorderid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.masterstatustransaksiorder_statustransorderid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: masterstatustransaksiorder_statustransorderid_seq1; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.masterstatustransaksiorder_statustransorderid_seq1
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    MAXVALUE 2147483647
    CACHE 1;


ALTER TABLE public.masterstatustransaksiorder_statustransorderid_seq1 OWNER TO postgres;

--
-- Name: promomenu; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.promomenu (
    promomenuid integer NOT NULL,
    productid integer NOT NULL,
    promoanimationurl character varying NOT NULL,
    cityid integer
);


ALTER TABLE public.promomenu OWNER TO postgres;

--
-- Name: promomenu_promomenuid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.promomenu ALTER COLUMN promomenuid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.promomenu_promomenuid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: punishment; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.punishment (
    punishmentid integer NOT NULL,
    userid integer NOT NULL,
    merchantid integer NOT NULL,
    orderheaderid integer NOT NULL
);


ALTER TABLE public.punishment OWNER TO postgres;

--
-- Name: punishment_punishmentid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.punishment ALTER COLUMN punishmentid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.punishment_punishmentid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: transabsensidropship; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.transabsensidropship (
    openpodetaildropshipid integer NOT NULL,
    openpoheaderid integer NOT NULL,
    latitude character varying(20) NOT NULL,
    longitude character varying(20) NOT NULL,
    address character varying NOT NULL,
    foto character varying,
    waktuentry timestamp without time zone NOT NULL
);


ALTER TABLE public.transabsensidropship OWNER TO postgres;

--
-- Name: transopenpodetaildropship; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.transopenpodetaildropship (
    openpodetaildropshipid integer NOT NULL,
    openpoheaderid integer NOT NULL,
    dropshipid integer NOT NULL,
    starttime character varying(10),
    endtime character varying(10),
    tolerance character varying,
    keterangan character varying NOT NULL,
    ongkoskirim numeric,
    iscod boolean DEFAULT false NOT NULL
);


ALTER TABLE public.transopenpodetaildropship OWNER TO postgres;

--
-- Name: transopenpodetaildropship_openpodetaildropshipid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.transopenpodetaildropship ALTER COLUMN openpodetaildropshipid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.transopenpodetaildropship_openpodetaildropshipid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: transopenpodetaildropshipkategori; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.transopenpodetaildropshipkategori (
    openpodetaildropshipid integer NOT NULL,
    categorymenuid integer NOT NULL
);


ALTER TABLE public.transopenpodetaildropshipkategori OWNER TO postgres;

--
-- Name: transopenpodetailproduct; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.transopenpodetailproduct (
    openpodetailproductid integer NOT NULL,
    openpodetaildropshipid integer NOT NULL,
    openpoheaderid integer NOT NULL,
    productid integer NOT NULL,
    productprice numeric NOT NULL,
    kuotamin numeric,
    kuotamax numeric,
    qtymax numeric,
    qtymin numeric
);


ALTER TABLE public.transopenpodetailproduct OWNER TO postgres;

--
-- Name: transopenpodetailproduct_openpodetailproductid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.transopenpodetailproduct ALTER COLUMN openpodetailproductid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.transopenpodetailproduct_openpodetailproductid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: transopenpoheader; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.transopenpoheader (
    openpoheaderid integer NOT NULL,
    openpodate timestamp without time zone NOT NULL,
    closepodate timestamp without time zone NOT NULL,
    kota character varying(25) NOT NULL,
    statustransaksi character varying(10) NOT NULL,
    waktuentry timestamp without time zone NOT NULL,
    merchantid integer NOT NULL
);


ALTER TABLE public.transopenpoheader OWNER TO postgres;

--
-- Name: transopenpoheader_openpoheaderid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.transopenpoheader ALTER COLUMN openpoheaderid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.transopenpoheader_openpoheaderid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: transorderdetail; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.transorderdetail (
    orderdetailid integer NOT NULL,
    orderheaderid integer NOT NULL,
    openpodetailproductid integer NOT NULL,
    qty numeric NOT NULL,
    harga numeric NOT NULL,
    subtotal numeric NOT NULL,
    notes character varying
);


ALTER TABLE public.transorderdetail OWNER TO postgres;

--
-- Name: transorderdetail_orderdetailid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.transorderdetail ALTER COLUMN orderdetailid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.transorderdetail_orderdetailid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: transorderheader; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.transorderheader (
    orderheaderid integer NOT NULL,
    openpoheaderid integer NOT NULL,
    openpodetaildropshipid integer NOT NULL,
    paymentmethodid integer,
    notransaksi character varying(30) NOT NULL,
    userentry integer NOT NULL,
    waktuentry timestamp without time zone NOT NULL,
    longitude character varying(20) NOT NULL,
    latitude character varying(20) NOT NULL,
    address character varying NOT NULL,
    total numeric NOT NULL,
    notesaddress character varying,
    statustransorderid integer NOT NULL,
    userclosed integer
);


ALTER TABLE public.transorderheader OWNER TO postgres;

--
-- Name: transorderheader_orderheaderid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.transorderheader ALTER COLUMN orderheaderid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.transorderheader_orderheaderid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: transpembayaran; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.transpembayaran (
    pembayaranid integer NOT NULL,
    orderheaderid integer NOT NULL,
    jumlahbayar numeric NOT NULL,
    digitbayar integer NOT NULL,
    idrekening integer NOT NULL,
    noinvoice character varying NOT NULL,
    noref character varying,
    buktibayarurl character varying,
    statuspembayaranid integer NOT NULL,
    waktubayar timestamp without time zone,
    waktuentry timestamp without time zone NOT NULL
);


ALTER TABLE public.transpembayaran OWNER TO postgres;

--
-- Name: transpembayaran_pembayaranid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.transpembayaran ALTER COLUMN pembayaranid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.transpembayaran_pembayaranid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: transpengiriman; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.transpengiriman (
    pengirimanid integer NOT NULL,
    orderheaderid integer NOT NULL,
    shipmentid integer NOT NULL,
    foto character varying,
    waktukirim timestamp without time zone NOT NULL,
    waktuterima timestamp without time zone,
    namapenerima character varying,
    fotobuktiterima character varying,
    keterangan character varying
);


ALTER TABLE public.transpengiriman OWNER TO postgres;

--
-- Name: transpengiriman_pengirimanid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.transpengiriman ALTER COLUMN pengirimanid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.transpengiriman_pengirimanid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    userid integer NOT NULL,
    email character varying(60) NOT NULL,
    password character varying(60) NOT NULL,
    firstname character varying(30) NOT NULL,
    lastname character varying(30),
    birthdate date,
    region character varying(10),
    gender character varying(10) NOT NULL,
    address character varying NOT NULL,
    postalcode character varying(8),
    cityid integer NOT NULL,
    phone character varying(18) NOT NULL,
    job character varying(20),
    avatarurl character varying,
    identitycardurl character varying,
    createddate timestamp without time zone,
    lastlogin timestamp without time zone,
    lastlogout timestamp without time zone,
    isverified boolean NOT NULL,
    ismerchant boolean NOT NULL,
    biometric character varying,
    islogin boolean,
    punishmentcount integer,
    isverifiedmerchant boolean
);


ALTER TABLE public.users OWNER TO postgres;

--
-- Name: users_userid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.users ALTER COLUMN userid ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.users_userid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Data for Name: bannermenu; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.bannermenu (bannermenuid, bannermenuname, bannerimageurl, cityid) FROM stdin;
\.


--
-- Data for Name: favoritproduct; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.favoritproduct (productid, userid, waktuentry) FROM stdin;
\.


--
-- Data for Name: mastercategorymenu; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.mastercategorymenu (categorymenuid, categorymenuname, categoryimageurl) FROM stdin;
1	Makanan	uri
\.


--
-- Data for Name: mastercounter; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.mastercounter (id, prefix, jenistransaksi, lastinc, lastid) FROM stdin;
1	ORD	TRANSAKSI ORDER	1	ORD0000000001
\.


--
-- Data for Name: masterdropship; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.masterdropship (dropshipid, merchantid, label, longitude, latitude, dropshipaddress, description, contactname, contactphone, radius, isactive, ongkoskirim, iscod) FROM stdin;
\.


--
-- Data for Name: masterkota; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.masterkota (id, idprovinsi, nama) FROM stdin;
1101	11	KABUPATEN SIMEULUE
1102	11	KABUPATEN ACEH SINGKIL
1103	11	KABUPATEN ACEH SELATA
1104	11	KABUPATEN ACEH TENGGARA
1105	11	KABUPATEN ACEH TIMUR
1106	11	KABUPATEN ACEH TENGAH
1107	11	KABUPATEN ACEH BARAT
1108	11	KABUPATEN ACEH BESAR
1109	11	KABUPATEN PIDIE
1110	11	KABUPATEN BIREUE
1111	11	KABUPATEN ACEH UTARA
1112	11	KABUPATEN ACEH BARAT DAYA
1113	11	KABUPATEN GAYO LUES
1114	11	KABUPATEN ACEH TAMIANG
1115	11	KABUPATEN NAGAN RAYA
1116	11	KABUPATEN ACEH JAYA
1117	11	KABUPATEN BENER MERIAH
1118	11	KABUPATEN PIDIE JAYA
1171	11	Kota BANDA ACEH
1172	11	Kota SABANG
1173	11	Kota LANGSA
1174	11	Kota LHOKSEUMAWE
1175	11	Kota SUBULUSSALAM
1201	12	KABUPATEN NIAS
1202	12	KABUPATEN MANDAILING NATAL
1203	12	KABUPATEN TAPANULI SELATA
1204	12	KABUPATEN TAPANULI TENGAH
1205	12	KABUPATEN TAPANULI UTARA
1206	12	KABUPATEN TOBA SAMOSIR
1207	12	KABUPATEN LABUHAN BATU
1208	12	KABUPATEN ASAHA
1209	12	KABUPATEN SIMALUNGU
1210	12	KABUPATEN DAIRI
1211	12	KABUPATEN KARO
1212	12	KABUPATEN DELI SERDANG
1213	12	KABUPATEN LANGKAT
1214	12	KABUPATEN NIAS SELATA
1215	12	KABUPATEN HUMBANG HASUNDUTA
1216	12	KABUPATEN PAKPAK BHARAT
1217	12	KABUPATEN SAMOSIR
1218	12	KABUPATEN SERDANG BEDAGAI
1219	12	KABUPATEN BATU BARA
1220	12	KABUPATEN PADANG LAWAS UTARA
1221	12	KABUPATEN PADANG LAWAS
1222	12	KABUPATEN LABUHAN BATU SELATA
1223	12	KABUPATEN LABUHAN BATU UTARA
1224	12	KABUPATEN NIAS UTARA
1225	12	KABUPATEN NIAS BARAT
1271	12	Kota SIBOLGA
1272	12	Kota TANJUNG BALAI
1273	12	Kota PEMATANG SIANTAR
1274	12	Kota TEBING TINGGI
1275	12	Kota MEDA
1276	12	Kota BINJAI
1277	12	Kota PADANGSIDIMPUA
1278	12	Kota GUNUNGSITOLI
1301	13	KABUPATEN KEPULAUAN MENTAWAI
1302	13	KABUPATEN PESISIR SELATA
1303	13	KABUPATEN SOLOK
1304	13	KABUPATEN SIJUNJUNG
1305	13	KABUPATEN TANAH DATAR
1306	13	KABUPATEN PADANG PARIAMA
1307	13	KABUPATEN AGAM
1308	13	KABUPATEN LIMA PULUH Kota
1309	13	KABUPATEN PASAMA
1310	13	KABUPATEN SOLOK SELATA
1311	13	KABUPATEN DHARMASRAYA
1312	13	KABUPATEN PASAMAN BARAT
1371	13	Kota PADANG
1372	13	Kota SOLOK
1373	13	Kota SAWAH LUNTO
1374	13	Kota PADANG PANJANG
1375	13	Kota BUKITTINGGI
1376	13	Kota PAYAKUMBUH
1377	13	Kota PARIAMA
1401	14	KABUPATEN KUANTAN SINGINGI
1402	14	KABUPATEN INDRAGIRI HULU
1403	14	KABUPATEN INDRAGIRI HILIR
1404	14	KABUPATEN PELALAWA
1405	14	KABUPATEN S I A K
1406	14	KABUPATEN KAMPAR
1407	14	KABUPATEN ROKAN HULU
1408	14	KABUPATEN BENGKALIS
1409	14	KABUPATEN ROKAN HILIR
1410	14	KABUPATEN KEPULAUAN MERANTI
1471	14	Kota PEKANBARU
1473	14	Kota D U M A I
1501	15	KABUPATEN KERINCI
1502	15	KABUPATEN MERANGI
1503	15	KABUPATEN SAROLANGU
1504	15	KABUPATEN BATANG HARI
1505	15	KABUPATEN MUARO JAMBI
1506	15	KABUPATEN TANJUNG JABUNG TIMUR
1507	15	KABUPATEN TANJUNG JABUNG BARAT
1508	15	KABUPATEN TEBO
1509	15	KABUPATEN BU
1571	15	Kota JAMBI
1572	15	Kota SUNGAI PENUH
1601	16	KABUPATEN OGAN KOMERING ULU
1602	16	KABUPATEN OGAN KOMERING ILIR
1603	16	KABUPATEN MUARA ENIM
1604	16	KABUPATEN LAHAT
1605	16	KABUPATEN MUSI RAWAS
1606	16	KABUPATEN MUSI BANYUASI
1607	16	KABUPATEN BANYU ASI
1608	16	KABUPATEN OGAN KOMERING ULU SELATA
1609	16	KABUPATEN OGAN KOMERING ULU TIMUR
1610	16	KABUPATEN OGAN ILIR
1611	16	KABUPATEN EMPAT LAWANG
1612	16	KABUPATEN PENUKAL ABAB LEMATANG ILIR
1613	16	KABUPATEN MUSI RAWAS UTARA
1671	16	Kota PALEMBANG
1672	16	Kota PRABUMULIH
1673	16	Kota PAGAR ALAM
1674	16	Kota LUBUKLINGGAU
1701	17	KABUPATEN BENGKULU SELATA
1702	17	KABUPATEN REJANG LEBONG
1703	17	KABUPATEN BENGKULU UTARA
1704	17	KABUPATEN KAUR
1705	17	KABUPATEN SELUMA
1706	17	KABUPATEN MUKOMUKO
1707	17	KABUPATEN LEBONG
1708	17	KABUPATEN KEPAHIANG
1709	17	KABUPATEN BENGKULU TENGAH
1771	17	Kota BENGKULU
1801	18	KABUPATEN LAMPUNG BARAT
1802	18	KABUPATEN TANGGAMUS
1803	18	KABUPATEN LAMPUNG SELATA
1804	18	KABUPATEN LAMPUNG TIMUR
1805	18	KABUPATEN LAMPUNG TENGAH
1806	18	KABUPATEN LAMPUNG UTARA
1807	18	KABUPATEN WAY KANA
1808	18	KABUPATEN TULANGBAWANG
1809	18	KABUPATEN PESAWARA
1810	18	KABUPATEN PRINGSEWU
1811	18	KABUPATEN MESUJI
1812	18	KABUPATEN TULANG BAWANG BARAT
1813	18	KABUPATEN PESISIR BARAT
1871	18	Kota BANDAR LAMPUNG
1872	18	Kota METRO
1901	19	KABUPATEN BANGKA
1902	19	KABUPATEN BELITUNG
1903	19	KABUPATEN BANGKA BARAT
1904	19	KABUPATEN BANGKA TENGAH
1905	19	KABUPATEN BANGKA SELATA
1906	19	KABUPATEN BELITUNG TIMUR
1971	19	Kota PANGKAL PINANG
2101	21	KABUPATEN KARIMU
2102	21	KABUPATEN BINTA
2103	21	KABUPATEN NATUNA
2104	21	KABUPATEN LINGGA
2105	21	KABUPATEN KEPULAUAN ANAMBAS
2171	21	Kota B A T A M
2172	21	Kota TANJUNG PINANG
3101	31	KABUPATEN KEPULAUAN SERIBU
3171	31	Kota JAKARTA SELATA
3172	31	Kota JAKARTA TIMUR
3173	31	Kota JAKARTA PUSAT
3174	31	Kota JAKARTA BARAT
3175	31	Kota JAKARTA UTARA
3201	32	KABUPATEN BOR
3202	32	KABUPATEN SUKABUMI
3203	32	KABUPATEN CIANJUR
3204	32	KABUPATEN BANDUNG
3205	32	KABUPATEN GARUT
3206	32	KABUPATEN TASIKMALAYA
3207	32	KABUPATEN CIAMIS
3208	32	KABUPATEN KUNINGA
3209	32	KABUPATEN CIREBO
3210	32	KABUPATEN MAJALENGKA
3211	32	KABUPATEN SUMEDANG
3212	32	KABUPATEN INDRAMAYU
3213	32	KABUPATEN SUBANG
3214	32	KABUPATEN PURWAKARTA
3215	32	KABUPATEN KARAWANG
3216	32	KABUPATEN BEKASI
3217	32	KABUPATEN BANDUNG BARAT
3218	32	KABUPATEN PANGANDARA
3271	32	Kota BOR
3272	32	Kota SUKABUMI
3273	32	Kota BANDUNG
3274	32	Kota CIREBO
3275	32	Kota BEKASI
3276	32	Kota DEPOK
3277	32	Kota CIMAHI
3278	32	Kota TASIKMALAYA
3279	32	Kota BANJAR
3301	33	KABUPATEN CILACAP
3302	33	KABUPATEN BANYUMAS
3303	33	KABUPATEN PURBALINGGA
3304	33	KABUPATEN BANJARNEGARA
3305	33	KABUPATEN KEBUME
3306	33	KABUPATEN PURWOREJO
3307	33	KABUPATEN WONOSOBO
3308	33	KABUPATEN MAGELANG
3309	33	KABUPATEN BOYOLALI
3310	33	KABUPATEN KLATE
3311	33	KABUPATEN SUKOHARJO
3312	33	KABUPATEN WONOGIRI
3313	33	KABUPATEN KARANGANYAR
3314	33	KABUPATEN SRAGE
3315	33	KABUPATEN GROBOGA
3316	33	KABUPATEN BLORA
3317	33	KABUPATEN REMBANG
3318	33	KABUPATEN PATI
3319	33	KABUPATEN KUDUS
3320	33	KABUPATEN JEPARA
3321	33	KABUPATEN DEMAK
3322	33	KABUPATEN SEMARANG
3323	33	KABUPATEN TEMANGGUNG
3324	33	KABUPATEN KENDAL
3325	33	KABUPATEN BATANG
3326	33	KABUPATEN PEKALONGA
3327	33	KABUPATEN PEMALANG
3328	33	KABUPATEN TEGAL
3329	33	KABUPATEN BREBES
3371	33	Kota MAGELANG
3372	33	Kota SURAKARTA
3373	33	Kota SALATIGA
3374	33	Kota SEMARANG
3375	33	Kota PEKALONGA
3376	33	Kota TEGAL
3401	34	KABUPATEN KULON PRO
3402	34	KABUPATEN BANTUL
3403	34	KABUPATEN GUNUNG KIDUL
3404	34	KABUPATEN SLEMA
3471	34	Kota YOGYAKARTA
3501	35	KABUPATEN PACITA
3502	35	KABUPATEN PONORO
3503	35	KABUPATEN TRENGGALEK
3504	35	KABUPATEN TULUNGAGUNG
3505	35	KABUPATEN BLITAR
3506	35	KABUPATEN KEDIRI
3507	35	KABUPATEN MALANG
3508	35	KABUPATEN LUMAJANG
3509	35	KABUPATEN JEMBER
3510	35	KABUPATEN BANYUWANGI
3511	35	KABUPATEN BONDOWOSO
3512	35	KABUPATEN SITUBONDO
3513	35	KABUPATEN PROBOLING
3514	35	KABUPATEN PASURUA
3515	35	KABUPATEN SIDOARJO
3516	35	KABUPATEN MOJOKERTO
3517	35	KABUPATEN JOMBANG
3518	35	KABUPATEN NGANJUK
3519	35	KABUPATEN MADIU
3520	35	KABUPATEN MAGETA
3521	35	KABUPATEN NGAWI
3522	35	KABUPATEN BOJONERO
3523	35	KABUPATEN TUBA
3524	35	KABUPATEN LAMONGA
3525	35	KABUPATEN GRESIK
3526	35	KABUPATEN BANGKALA
3527	35	KABUPATEN SAMPANG
3528	35	KABUPATEN PAMEKASA
3529	35	KABUPATEN SUMENEP
3571	35	Kota KEDIRI
3572	35	Kota BLITAR
3573	35	Kota MALANG
3574	35	Kota PROBOLING
3575	35	Kota PASURUA
3576	35	Kota MOJOKERTO
3577	35	Kota MADIU
3578	35	Kota SURABAYA
3579	35	Kota BATU
3601	36	KABUPATEN PANDEGLANG
3602	36	KABUPATEN LEBAK
3603	36	KABUPATEN TANGERANG
3604	36	KABUPATEN SERANG
3671	36	Kota TANGERANG
3672	36	Kota CILE
3673	36	Kota SERANG
3674	36	Kota TANGERANG SELATA
5101	51	KABUPATEN JEMBRANA
5102	51	KABUPATEN TABANA
5103	51	KABUPATEN BADUNG
5104	51	KABUPATEN GIANYAR
5105	51	KABUPATEN KLUNGKUNG
5106	51	KABUPATEN BANGLI
5107	51	KABUPATEN KARANG ASEM
5108	51	KABUPATEN BULELENG
5171	51	Kota DENPASAR
5201	52	KABUPATEN LOMBOK BARAT
5202	52	KABUPATEN LOMBOK TENGAH
5203	52	KABUPATEN LOMBOK TIMUR
5204	52	KABUPATEN SUMBAWA
5205	52	KABUPATEN DOMPU
5206	52	KABUPATEN BIMA
5207	52	KABUPATEN SUMBAWA BARAT
5208	52	KABUPATEN LOMBOK UTARA
5271	52	Kota MATARAM
5272	52	Kota BIMA
5301	53	KABUPATEN SUMBA BARAT
5302	53	KABUPATEN SUMBA TIMUR
5303	53	KABUPATEN KUPANG
5304	53	KABUPATEN TIMOR TENGAH SELATA
5305	53	KABUPATEN TIMOR TENGAH UTARA
5306	53	KABUPATEN BELU
5307	53	KABUPATEN ALOR
5308	53	KABUPATEN LEMBATA
5309	53	KABUPATEN FLORES TIMUR
5310	53	KABUPATEN SIKKA
5311	53	KABUPATEN ENDE
5312	53	KABUPATEN NGADA
5313	53	KABUPATEN MANGGARAI
5314	53	KABUPATEN ROTE NDAO
5315	53	KABUPATEN MANGGARAI BARAT
5316	53	KABUPATEN SUMBA TENGAH
5317	53	KABUPATEN SUMBA BARAT DAYA
5318	53	KABUPATEN NAGEKEO
5319	53	KABUPATEN MANGGARAI TIMUR
5320	53	KABUPATEN SABU RAIJUA
5321	53	KABUPATEN MALAKA
5371	53	Kota KUPANG
6101	61	KABUPATEN SAMBAS
6102	61	KABUPATEN BENGKAYANG
6103	61	KABUPATEN LANDAK
6104	61	KABUPATEN MEMPAWAH
6105	61	KABUPATEN SANGGAU
6106	61	KABUPATEN KETAPANG
6107	61	KABUPATEN SINTANG
6108	61	KABUPATEN KAPUAS HULU
6109	61	KABUPATEN SEKADAU
6110	61	KABUPATEN MELAWI
6111	61	KABUPATEN KAYONG UTARA
6112	61	KABUPATEN KUBU RAYA
6171	61	Kota PONTIANAK
6172	61	Kota SINGKAWANG
6201	62	KABUPATEN KotaWARINGIN BARAT
6202	62	KABUPATEN KotaWARINGIN TIMUR
6203	62	KABUPATEN KAPUAS
6204	62	KABUPATEN BARITO SELATA
6205	62	KABUPATEN BARITO UTARA
6206	62	KABUPATEN SUKAMARA
6207	62	KABUPATEN LAMANDAU
6208	62	KABUPATEN SERUYA
6209	62	KABUPATEN KATINGA
6210	62	KABUPATEN PULANG PISAU
6211	62	KABUPATEN GUNUNG MAS
6212	62	KABUPATEN BARITO TIMUR
6213	62	KABUPATEN MURUNG RAYA
6271	62	Kota PALANGKA RAYA
6301	63	KABUPATEN TANAH LAUT
6302	63	KABUPATEN Kota BARU
6303	63	KABUPATEN BANJAR
6304	63	KABUPATEN BARITO KUALA
6305	63	KABUPATEN TAPI
6306	63	KABUPATEN HULU SUNGAI SELATA
6307	63	KABUPATEN HULU SUNGAI TENGAH
6308	63	KABUPATEN HULU SUNGAI UTARA
6309	63	KABUPATEN TABALONG
6310	63	KABUPATEN TANAH BUMBU
6311	63	KABUPATEN BALANGA
6371	63	Kota BANJARMASI
6372	63	Kota BANJAR BARU
6401	64	KABUPATEN PASER
6402	64	KABUPATEN KUTAI BARAT
6403	64	KABUPATEN KUTAI KARTANEGARA
6404	64	KABUPATEN KUTAI TIMUR
6405	64	KABUPATEN BERAU
6409	64	KABUPATEN PENAJAM PASER UTARA
6411	64	KABUPATEN MAHAKAM HULU
6471	64	Kota BALIKPAPA
6472	64	Kota SAMARINDA
6474	64	Kota BONTANG
6501	65	KABUPATEN MALINAU
6502	65	KABUPATEN BULUNGA
6503	65	KABUPATEN TANA TIDUNG
6504	65	KABUPATEN NUNUKA
6571	65	Kota TARAKA
7101	71	KABUPATEN BOLAANG MONNDOW
7102	71	KABUPATEN MINAHASA
7103	71	KABUPATEN KEPULAUAN SANGIHE
7104	71	KABUPATEN KEPULAUAN TALAUD
7105	71	KABUPATEN MINAHASA SELATA
7106	71	KABUPATEN MINAHASA UTARA
7107	71	KABUPATEN BOLAANG MONNDOW UTARA
7108	71	KABUPATEN SIAU TAGULANDANG BIARO
7109	71	KABUPATEN MINAHASA TENGGARA
7110	71	KABUPATEN BOLAANG MONNDOW SELATA
7111	71	KABUPATEN BOLAANG MONNDOW TIMUR
7171	71	Kota MANADO
7172	71	Kota BITUNG
7173	71	Kota TOMOHO
7174	71	Kota KotaMOBAGU
7201	72	KABUPATEN BANGGAI KEPULAUA
7202	72	KABUPATEN BANGGAI
7203	72	KABUPATEN MOROWALI
7204	72	KABUPATEN POSO
7205	72	KABUPATEN DONGGALA
7206	72	KABUPATEN TOLI-TOLI
7207	72	KABUPATEN BUOL
7208	72	KABUPATEN PARIGI MOUTONG
7209	72	KABUPATEN TOJO UNA-UNA
7210	72	KABUPATEN SIGI
7211	72	KABUPATEN BANGGAI LAUT
7212	72	KABUPATEN MOROWALI UTARA
7271	72	Kota PALU
7301	73	KABUPATEN KEPULAUAN SELAYAR
7302	73	KABUPATEN BULUKUMBA
7303	73	KABUPATEN BANTAENG
7304	73	KABUPATEN JENEPONTO
7305	73	KABUPATEN TAKALAR
7306	73	KABUPATEN WA
7307	73	KABUPATEN SINJAI
7308	73	KABUPATEN MAROS
7309	73	KABUPATEN PANGKAJENE DAN KEPULAUA
7310	73	KABUPATEN BARRU
7311	73	KABUPATEN BONE
7312	73	KABUPATEN SOPPENG
7313	73	KABUPATEN WAJO
7314	73	KABUPATEN SIDENRENG RAPPANG
7315	73	KABUPATEN PINRANG
7316	73	KABUPATEN ENREKANG
7317	73	KABUPATEN LUWU
7318	73	KABUPATEN TANA TORAJA
7322	73	KABUPATEN LUWU UTARA
7325	73	KABUPATEN LUWU TIMUR
7326	73	KABUPATEN TORAJA UTARA
7371	73	Kota MAKASSAR
7372	73	Kota PAREPARE
7373	73	Kota PALOPO
7401	74	KABUPATEN BUTO
7402	74	KABUPATEN MUNA
7403	74	KABUPATEN KONAWE
7404	74	KABUPATEN KOLAKA
7405	74	KABUPATEN KONAWE SELATA
7406	74	KABUPATEN BOMBANA
7407	74	KABUPATEN WAKATOBI
7408	74	KABUPATEN KOLAKA UTARA
7409	74	KABUPATEN BUTON UTARA
7410	74	KABUPATEN KONAWE UTARA
7411	74	KABUPATEN KOLAKA TIMUR
7412	74	KABUPATEN KONAWE KEPULAUA
7413	74	KABUPATEN MUNA BARAT
7414	74	KABUPATEN BUTON TENGAH
7415	74	KABUPATEN BUTON SELATA
7471	74	Kota KENDARI
7472	74	Kota BAUBAU
7501	75	KABUPATEN BOALEMO
7502	75	KABUPATEN RONTALO
7503	75	KABUPATEN POHUWATO
7504	75	KABUPATEN BONE BOLA
7505	75	KABUPATEN RONTALO UTARA
7571	75	Kota RONTALO
7601	76	KABUPATEN MAJENE
7602	76	KABUPATEN POLEWALI MANDAR
7603	76	KABUPATEN MAMASA
7604	76	KABUPATEN MAMUJU
7605	76	KABUPATEN MAMUJU UTARA
7606	76	KABUPATEN MAMUJU TENGAH
8101	81	KABUPATEN MALUKU TENGGARA BARAT
8102	81	KABUPATEN MALUKU TENGGARA
8103	81	KABUPATEN MALUKU TENGAH
8104	81	KABUPATEN BURU
8105	81	KABUPATEN KEPULAUAN ARU
8106	81	KABUPATEN SERAM BAGIAN BARAT
8107	81	KABUPATEN SERAM BAGIAN TIMUR
8108	81	KABUPATEN MALUKU BARAT DAYA
8109	81	KABUPATEN BURU SELATA
8171	81	Kota AMBO
8172	81	Kota TUAL
8201	82	KABUPATEN HALMAHERA BARAT
8202	82	KABUPATEN HALMAHERA TENGAH
8203	82	KABUPATEN KEPULAUAN SULA
8204	82	KABUPATEN HALMAHERA SELATA
8205	82	KABUPATEN HALMAHERA UTARA
8206	82	KABUPATEN HALMAHERA TIMUR
8207	82	KABUPATEN PULAU MOROTAI
8208	82	KABUPATEN PULAU TALIABU
8271	82	Kota TERNATE
8272	82	Kota TIDORE KEPULAUA
9101	91	KABUPATEN FAKFAK
9102	91	KABUPATEN KAIMANA
9103	91	KABUPATEN TELUK WONDAMA
9104	91	KABUPATEN TELUK BINTUNI
9105	91	KABUPATEN MANOKWARI
9106	91	KABUPATEN SORONG SELATA
9107	91	KABUPATEN SORONG
9108	91	KABUPATEN RAJA AMPAT
9109	91	KABUPATEN TAMBRAUW
9110	91	KABUPATEN MAYBRAT
9111	91	KABUPATEN MANOKWARI SELATA
9112	91	KABUPATEN PEGUNUNGAN ARFAK
9171	91	Kota SORONG
9401	94	KABUPATEN MERAUKE
9402	94	KABUPATEN JAYAWIJAYA
9403	94	KABUPATEN JAYAPURA
9404	94	KABUPATEN NABIRE
9408	94	KABUPATEN KEPULAUAN YAPE
9409	94	KABUPATEN BIAK NUMFOR
9410	94	KABUPATEN PANIAI
9411	94	KABUPATEN PUNCAK JAYA
9412	94	KABUPATEN MIMIKA
9413	94	KABUPATEN BOVEN DIEL
9414	94	KABUPATEN MAPPI
9415	94	KABUPATEN ASMAT
9416	94	KABUPATEN YAHUKIMO
9417	94	KABUPATEN PEGUNUNGAN BINTANG
9418	94	KABUPATEN TOLIKARA
9419	94	KABUPATEN SARMI
9420	94	KABUPATEN KEEROM
9426	94	KABUPATEN WAROPE
9427	94	KABUPATEN SUPIORI
9428	94	KABUPATEN MAMBERAMO RAYA
9429	94	KABUPATEN NDUGA
9430	94	KABUPATEN LANNY JAYA
9431	94	KABUPATEN MAMBERAMO TENGAH
9432	94	KABUPATEN YALIMO
9433	94	KABUPATEN PUNCAK
9434	94	KABUPATEN DOGIYAI
9435	94	KABUPATEN INTAN JAYA
9436	94	KABUPATEN DEIYAI
9471	94	Kota JAYAPURA
\.


--
-- Data for Name: mastermerchant; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.mastermerchant (merchantid, userid, merchantname, description, logoimageurl, coverimageurl, avgratingproduct, avgratingpackaging, avgratingdelivering, namabank, nomorrekening, namapemilikrekening) FROM stdin;
1	2	Toko Maju	Toko yang menjual berbagai macam bahan makanan	uri	uri	0	0	0	\N	\N	\N
\.


--
-- Data for Name: masterpaymentmethod; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.masterpaymentmethod (paymentmethodid, keterangan) FROM stdin;
\.


--
-- Data for Name: masterproduct; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.masterproduct (productid, merchantid, categorymenuid, productname, productimageurl, productprice, productdescription, isbutton, isactive, productsatuan, kuota, kuotamax, kuotamin, qtymax, qtymin, ishalal) FROM stdin;
2	1	1	Nasi kucing bumbu rawon	uri	19500.00	Nasi kucing yang dibumbui dengan bumbu rawon, rasanya gurih dijamin gak nyesel	t	t	\N	100	\N	\N	\N	\N	t
\.


--
-- Data for Name: masterprovinsi; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.masterprovinsi (id, nama) FROM stdin;
11	ACEH
12	SUMATERA UTARA
13	SUMATERA BARAT
14	RIAU
15	JAMBI
16	SUMATERA SELATA
17	BENGKULU
18	LAMPUNG
19	KEPULAUAN BANGKA BELITUNG
21	KEPULAUAN RIAU
31	DKI JAKARTA
32	JAWA BARAT
33	JAWA TENGAH
34	DI YOGYAKARTA
35	JAWA TIMUR
36	BANTE
51	BALI
52	NUSA TENGGARA BARAT
53	NUSA TENGGARA TIMUR
61	KALIMANTAN BARAT
62	KALIMANTAN TENGAH
63	KALIMANTAN SELATA
64	KALIMANTAN TIMUR
65	KALIMANTAN UTARA
71	SULAWESI UTARA
72	SULAWESI TENGAH
73	SULAWESI SELATA
74	SULAWESI TENGGARA
75	RONTALO
76	SULAWESI BARAT
81	MALUKU
82	MALUKU UTARA
91	PAPUA BARAT
94	PAPUA
\.


--
-- Data for Name: masterratingdelivering; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.masterratingdelivering (ratingdeliveringid, rating, userentry, waktuentry, merchantid) FROM stdin;
\.


--
-- Data for Name: masterratingpackaging; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.masterratingpackaging (ratingpackagingid, rating, userentry, waktuentry, merchantid) FROM stdin;
\.


--
-- Data for Name: masterratingproduct; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.masterratingproduct (ratingproductid, rating, userentry, waktuentry, merchantid) FROM stdin;
\.


--
-- Data for Name: masterrekening; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.masterrekening (idrekening, norekening, namapemilikrekening, namabank) FROM stdin;
\.


--
-- Data for Name: mastershipment; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.mastershipment (shipmentid, keterangan) FROM stdin;
\.


--
-- Data for Name: masterstatuspembayaran; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.masterstatuspembayaran (statuspembayaranid, keterangan) FROM stdin;
\.


--
-- Data for Name: masterstatustransaksiorder; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.masterstatustransaksiorder (statustransorderid, keterangan) FROM stdin;
\.


--
-- Data for Name: promomenu; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.promomenu (promomenuid, productid, promoanimationurl, cityid) FROM stdin;
\.


--
-- Data for Name: punishment; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.punishment (punishmentid, userid, merchantid, orderheaderid) FROM stdin;
\.


--
-- Data for Name: transabsensidropship; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.transabsensidropship (openpodetaildropshipid, openpoheaderid, latitude, longitude, address, foto, waktuentry) FROM stdin;
\.


--
-- Data for Name: transopenpodetaildropship; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.transopenpodetaildropship (openpodetaildropshipid, openpoheaderid, dropshipid, starttime, endtime, tolerance, keterangan, ongkoskirim, iscod) FROM stdin;
\.


--
-- Data for Name: transopenpodetaildropshipkategori; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.transopenpodetaildropshipkategori (openpodetaildropshipid, categorymenuid) FROM stdin;
\.


--
-- Data for Name: transopenpodetailproduct; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.transopenpodetailproduct (openpodetailproductid, openpodetaildropshipid, openpoheaderid, productid, productprice, kuotamin, kuotamax, qtymax, qtymin) FROM stdin;
\.


--
-- Data for Name: transopenpoheader; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.transopenpoheader (openpoheaderid, openpodate, closepodate, kota, statustransaksi, waktuentry, merchantid) FROM stdin;
\.


--
-- Data for Name: transorderdetail; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.transorderdetail (orderdetailid, orderheaderid, openpodetailproductid, qty, harga, subtotal, notes) FROM stdin;
\.


--
-- Data for Name: transorderheader; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.transorderheader (orderheaderid, openpoheaderid, openpodetaildropshipid, paymentmethodid, notransaksi, userentry, waktuentry, longitude, latitude, address, total, notesaddress, statustransorderid, userclosed) FROM stdin;
\.


--
-- Data for Name: transpembayaran; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.transpembayaran (pembayaranid, orderheaderid, jumlahbayar, digitbayar, idrekening, noinvoice, noref, buktibayarurl, statuspembayaranid, waktubayar, waktuentry) FROM stdin;
\.


--
-- Data for Name: transpengiriman; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.transpengiriman (pengirimanid, orderheaderid, shipmentid, foto, waktukirim, waktuterima, namapenerima, fotobuktiterima, keterangan) FROM stdin;
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users (userid, email, password, firstname, lastname, birthdate, region, gender, address, postalcode, cityid, phone, job, avatarurl, identitycardurl, createddate, lastlogin, lastlogout, isverified, ismerchant, biometric, islogin, punishmentcount, isverifiedmerchant) FROM stdin;
4	wawan@gmail.com	tes	wawan	tes	1995-02-03	islam	l	jl	50276	0					2021-02-16 16:28:54.674933	2021-02-18 10:24:39.104398	2021-02-18 10:26:53.069561	t	f	tes sidik	\N	\N	\N
3	yusronn@gmail.com	tes	Aji	Bagus Pamungkas	1992-09-04	islam	L	jalan wonosari 3	50244	3			https://cdn.idntimes.com/content-images/post/20171214/22860372-875758699253075-7073222266430947328-n-6dbabff7de592f046f6a45b9ca9ea232-1059c8c5073f0d8b2a9682c79b504c9a.jpg	https://cdn.idntimes.com/content-images/post/20171214/22860372-875758699253075-7073222266430947328-n-6dbabff7de592f046f6a45b9ca9ea232-1059c8c5073f0d8b2a9682c79b504c9a.jpg	2021-02-16 11:45:13.607661	2021-02-23 12:00:09.140828	2021-02-18 10:24:06.888799	t	f	\N	\N	\N	\N
2	yusronn@gmail.com	tes	yusron	nasrullah	1995-07-04	islam	L	jalan wonosari 3	50244	3			https://cdn.idntimes.com/content-images/post/20171214/22860372-875758699253075-7073222266430947328-n-6dbabff7de592f046f6a45b9ca9ea232-1059c8c5073f0d8b2a9682c79b504c9a.jpg	https://cdn.idntimes.com/content-images/post/20171214/22860372-875758699253075-7073222266430947328-n-6dbabff7de592f046f6a45b9ca9ea232-1059c8c5073f0d8b2a9682c79b504c9a.jpg	2021-02-16 11:44:59.367151	2021-02-23 12:02:24.590921	2021-02-16 15:59:09.882652	t	f	tes biometrik yusron	\N	\N	\N
\.


--
-- Name: bannermenu_bannermenuid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.bannermenu_bannermenuid_seq', 2, true);


--
-- Name: mastercategorymenu_categorymenuid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.mastercategorymenu_categorymenuid_seq', 2, true);


--
-- Name: mastercounter_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.mastercounter_id_seq', 1, true);


--
-- Name: masterdropship_dropshipid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.masterdropship_dropshipid_seq', 1, false);


--
-- Name: mastermerchant_merchantid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.mastermerchant_merchantid_seq', 2, true);


--
-- Name: masterpaymentmethod_paymentmethodid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.masterpaymentmethod_paymentmethodid_seq', 1, false);


--
-- Name: masterproduct_productid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.masterproduct_productid_seq', 2, true);


--
-- Name: masterratingdelivering_ratingdeliveringid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.masterratingdelivering_ratingdeliveringid_seq', 1, false);


--
-- Name: masterratingpackaging_ratingpackagingid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.masterratingpackaging_ratingpackagingid_seq', 1, false);


--
-- Name: masterratingproduct_ratingproductid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.masterratingproduct_ratingproductid_seq', 1, false);


--
-- Name: masterrekening_idrekening_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.masterrekening_idrekening_seq', 1, false);


--
-- Name: mastershipment_shipmentid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.mastershipment_shipmentid_seq', 1, false);


--
-- Name: masterstatuspembayaran_statuspembayaranid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.masterstatuspembayaran_statuspembayaranid_seq', 1, false);


--
-- Name: masterstatustransaksiorder_statustransorderid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.masterstatustransaksiorder_statustransorderid_seq', 1, false);


--
-- Name: masterstatustransaksiorder_statustransorderid_seq1; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.masterstatustransaksiorder_statustransorderid_seq1', 1, false);


--
-- Name: promomenu_promomenuid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.promomenu_promomenuid_seq', 1, true);


--
-- Name: punishment_punishmentid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.punishment_punishmentid_seq', 1, false);


--
-- Name: transopenpodetaildropship_openpodetaildropshipid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.transopenpodetaildropship_openpodetaildropshipid_seq', 1, false);


--
-- Name: transopenpodetailproduct_openpodetailproductid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.transopenpodetailproduct_openpodetailproductid_seq', 1, false);


--
-- Name: transopenpoheader_openpoheaderid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.transopenpoheader_openpoheaderid_seq', 1, false);


--
-- Name: transorderdetail_orderdetailid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.transorderdetail_orderdetailid_seq', 1, false);


--
-- Name: transorderheader_orderheaderid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.transorderheader_orderheaderid_seq', 1, false);


--
-- Name: transpembayaran_pembayaranid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.transpembayaran_pembayaranid_seq', 1, false);


--
-- Name: transpengiriman_pengirimanid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.transpengiriman_pengirimanid_seq', 1, false);


--
-- Name: users_userid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.users_userid_seq', 8, true);


--
-- Name: bannermenu bannermenu_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.bannermenu
    ADD CONSTRAINT bannermenu_pkey PRIMARY KEY (bannermenuid);


--
-- Name: favoritproduct favoritproduct_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.favoritproduct
    ADD CONSTRAINT favoritproduct_pkey PRIMARY KEY (productid, userid);


--
-- Name: masterkota kota_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterkota
    ADD CONSTRAINT kota_pkey PRIMARY KEY (id);


--
-- Name: mastercategorymenu mastercategorymenu_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.mastercategorymenu
    ADD CONSTRAINT mastercategorymenu_pkey PRIMARY KEY (categorymenuid);


--
-- Name: mastercounter mastercounter_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.mastercounter
    ADD CONSTRAINT mastercounter_pkey PRIMARY KEY (id);


--
-- Name: masterdropship masterdropship_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterdropship
    ADD CONSTRAINT masterdropship_pkey PRIMARY KEY (dropshipid);


--
-- Name: mastermerchant mastermerchant_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.mastermerchant
    ADD CONSTRAINT mastermerchant_pkey PRIMARY KEY (merchantid);


--
-- Name: masterpaymentmethod masterpaymentmethod_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterpaymentmethod
    ADD CONSTRAINT masterpaymentmethod_pkey PRIMARY KEY (paymentmethodid);


--
-- Name: masterproduct masterproduct_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterproduct
    ADD CONSTRAINT masterproduct_pkey PRIMARY KEY (productid);


--
-- Name: masterratingdelivering masterratingdelivering_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterratingdelivering
    ADD CONSTRAINT masterratingdelivering_pkey PRIMARY KEY (ratingdeliveringid);


--
-- Name: masterratingpackaging masterratingpackaging_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterratingpackaging
    ADD CONSTRAINT masterratingpackaging_pkey PRIMARY KEY (ratingpackagingid);


--
-- Name: masterratingproduct masterratingproduct_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterratingproduct
    ADD CONSTRAINT masterratingproduct_pkey PRIMARY KEY (ratingproductid);


--
-- Name: masterrekening masterrekening_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterrekening
    ADD CONSTRAINT masterrekening_pkey PRIMARY KEY (idrekening);


--
-- Name: mastershipment mastershipment_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.mastershipment
    ADD CONSTRAINT mastershipment_pkey PRIMARY KEY (shipmentid);


--
-- Name: masterstatuspembayaran masterstatuspembayaran_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterstatuspembayaran
    ADD CONSTRAINT masterstatuspembayaran_pkey PRIMARY KEY (statuspembayaranid);


--
-- Name: masterstatustransaksiorder masterstatustransaksiorder_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterstatustransaksiorder
    ADD CONSTRAINT masterstatustransaksiorder_pkey PRIMARY KEY (statustransorderid);


--
-- Name: promomenu promomenu_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.promomenu
    ADD CONSTRAINT promomenu_pkey PRIMARY KEY (promomenuid);


--
-- Name: masterprovinsi provinsi_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterprovinsi
    ADD CONSTRAINT provinsi_pkey PRIMARY KEY (id);


--
-- Name: punishment punishment_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.punishment
    ADD CONSTRAINT punishment_pkey PRIMARY KEY (punishmentid);


--
-- Name: transopenpodetaildropship transopenpodetaildropship_openpodetaildropshipid_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transopenpodetaildropship
    ADD CONSTRAINT transopenpodetaildropship_openpodetaildropshipid_key UNIQUE (openpodetaildropshipid);


--
-- Name: transopenpodetaildropship transopenpodetaildropship_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transopenpodetaildropship
    ADD CONSTRAINT transopenpodetaildropship_pkey PRIMARY KEY (openpodetaildropshipid, openpoheaderid);


--
-- Name: transopenpodetaildropshipkategori transopenpodetaildropshipkategori_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transopenpodetaildropshipkategori
    ADD CONSTRAINT transopenpodetaildropshipkategori_pkey PRIMARY KEY (openpodetaildropshipid, categorymenuid);


--
-- Name: transopenpodetailproduct transopenpodetailproduct_openpodetailproductid_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transopenpodetailproduct
    ADD CONSTRAINT transopenpodetailproduct_openpodetailproductid_key UNIQUE (openpodetailproductid);


--
-- Name: transopenpodetailproduct transopenpodetailproduct_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transopenpodetailproduct
    ADD CONSTRAINT transopenpodetailproduct_pkey PRIMARY KEY (openpodetailproductid, openpodetaildropshipid, openpoheaderid);


--
-- Name: transopenpoheader transopenpoheader_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transopenpoheader
    ADD CONSTRAINT transopenpoheader_pkey PRIMARY KEY (openpoheaderid);


--
-- Name: transorderdetail transorderdetail_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transorderdetail
    ADD CONSTRAINT transorderdetail_pkey PRIMARY KEY (orderdetailid);


--
-- Name: transorderheader transorderheader_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transorderheader
    ADD CONSTRAINT transorderheader_pkey PRIMARY KEY (orderheaderid);


--
-- Name: transpembayaran transpembayaran_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transpembayaran
    ADD CONSTRAINT transpembayaran_pkey PRIMARY KEY (pembayaranid);


--
-- Name: transpengiriman transpengiriman_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transpengiriman
    ADD CONSTRAINT transpengiriman_pkey PRIMARY KEY (pengirimanid);


--
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (userid);


--
-- Name: idx; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX idx ON public.masterkota USING btree (id);

ALTER TABLE public.masterkota CLUSTER ON idx;


--
-- Name: idx_fk; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_fk ON public.masterkota USING btree (idprovinsi);


--
-- Name: favoritproduct favoritproduct_productid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.favoritproduct
    ADD CONSTRAINT favoritproduct_productid_fkey FOREIGN KEY (productid) REFERENCES public.masterproduct(productid);


--
-- Name: favoritproduct favoritproduct_userid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.favoritproduct
    ADD CONSTRAINT favoritproduct_userid_fkey FOREIGN KEY (userid) REFERENCES public.users(userid);


--
-- Name: masterdropship masterdropship_merchantid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterdropship
    ADD CONSTRAINT masterdropship_merchantid_fkey FOREIGN KEY (merchantid) REFERENCES public.mastermerchant(merchantid);


--
-- Name: mastermerchant mastermerchant_userid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.mastermerchant
    ADD CONSTRAINT mastermerchant_userid_fkey FOREIGN KEY (userid) REFERENCES public.users(userid);


--
-- Name: masterproduct masterproduct_categorymenuid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterproduct
    ADD CONSTRAINT masterproduct_categorymenuid_fkey FOREIGN KEY (categorymenuid) REFERENCES public.mastercategorymenu(categorymenuid);


--
-- Name: masterproduct masterproduct_merchantid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterproduct
    ADD CONSTRAINT masterproduct_merchantid_fkey FOREIGN KEY (merchantid) REFERENCES public.mastermerchant(merchantid);


--
-- Name: masterratingdelivering masterratingdelivering_merchantid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterratingdelivering
    ADD CONSTRAINT masterratingdelivering_merchantid_fkey FOREIGN KEY (merchantid) REFERENCES public.mastermerchant(merchantid);


--
-- Name: masterratingdelivering masterratingdelivering_userentry_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterratingdelivering
    ADD CONSTRAINT masterratingdelivering_userentry_fkey FOREIGN KEY (userentry) REFERENCES public.users(userid);


--
-- Name: masterratingpackaging masterratingpackaging_merchantid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterratingpackaging
    ADD CONSTRAINT masterratingpackaging_merchantid_fkey FOREIGN KEY (merchantid) REFERENCES public.mastermerchant(merchantid);


--
-- Name: masterratingpackaging masterratingpackaging_userentry_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterratingpackaging
    ADD CONSTRAINT masterratingpackaging_userentry_fkey FOREIGN KEY (userentry) REFERENCES public.users(userid);


--
-- Name: masterratingproduct masterratingproduct_merchantid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterratingproduct
    ADD CONSTRAINT masterratingproduct_merchantid_fkey FOREIGN KEY (merchantid) REFERENCES public.mastermerchant(merchantid);


--
-- Name: masterratingproduct masterratingproduct_userentry_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.masterratingproduct
    ADD CONSTRAINT masterratingproduct_userentry_fkey FOREIGN KEY (userentry) REFERENCES public.users(userid);


--
-- Name: promomenu promomenu_productid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.promomenu
    ADD CONSTRAINT promomenu_productid_fkey FOREIGN KEY (productid) REFERENCES public.masterproduct(productid);


--
-- Name: punishment punishment_merchantid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.punishment
    ADD CONSTRAINT punishment_merchantid_fkey FOREIGN KEY (merchantid) REFERENCES public.mastermerchant(merchantid);


--
-- Name: punishment punishment_orderheaderid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.punishment
    ADD CONSTRAINT punishment_orderheaderid_fkey FOREIGN KEY (orderheaderid) REFERENCES public.transorderheader(orderheaderid);


--
-- Name: punishment punishment_userid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.punishment
    ADD CONSTRAINT punishment_userid_fkey FOREIGN KEY (userid) REFERENCES public.users(userid);


--
-- Name: transabsensidropship transabsensidropship_openpodetaildropshipid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transabsensidropship
    ADD CONSTRAINT transabsensidropship_openpodetaildropshipid_fkey FOREIGN KEY (openpodetaildropshipid) REFERENCES public.transopenpodetaildropship(openpodetaildropshipid);


--
-- Name: transabsensidropship transabsensidropship_openpoheaderid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transabsensidropship
    ADD CONSTRAINT transabsensidropship_openpoheaderid_fkey FOREIGN KEY (openpoheaderid) REFERENCES public.transopenpoheader(openpoheaderid);


--
-- Name: transopenpodetaildropship transopenpodetaildropship_dropshipid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transopenpodetaildropship
    ADD CONSTRAINT transopenpodetaildropship_dropshipid_fkey FOREIGN KEY (dropshipid) REFERENCES public.masterdropship(dropshipid);


--
-- Name: transopenpodetaildropship transopenpodetaildropship_openpoheaderid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transopenpodetaildropship
    ADD CONSTRAINT transopenpodetaildropship_openpoheaderid_fkey FOREIGN KEY (openpoheaderid) REFERENCES public.transopenpoheader(openpoheaderid);


--
-- Name: transopenpodetaildropshipkategori transopenpodetaildropshipkategori_categorymenuid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transopenpodetaildropshipkategori
    ADD CONSTRAINT transopenpodetaildropshipkategori_categorymenuid_fkey FOREIGN KEY (categorymenuid) REFERENCES public.mastercategorymenu(categorymenuid);


--
-- Name: transopenpodetaildropshipkategori transopenpodetaildropshipkategori_openpodetaildropshipid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transopenpodetaildropshipkategori
    ADD CONSTRAINT transopenpodetaildropshipkategori_openpodetaildropshipid_fkey FOREIGN KEY (openpodetaildropshipid) REFERENCES public.transopenpodetaildropship(openpodetaildropshipid);


--
-- Name: transopenpodetailproduct transopenpodetailproduct_openpodetaildropshipid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transopenpodetailproduct
    ADD CONSTRAINT transopenpodetailproduct_openpodetaildropshipid_fkey FOREIGN KEY (openpodetaildropshipid) REFERENCES public.transopenpodetaildropship(openpodetaildropshipid);


--
-- Name: transopenpodetailproduct transopenpodetailproduct_openpoheaderid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transopenpodetailproduct
    ADD CONSTRAINT transopenpodetailproduct_openpoheaderid_fkey FOREIGN KEY (openpoheaderid) REFERENCES public.transopenpoheader(openpoheaderid);


--
-- Name: transopenpodetailproduct transopenpodetailproduct_productid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transopenpodetailproduct
    ADD CONSTRAINT transopenpodetailproduct_productid_fkey FOREIGN KEY (productid) REFERENCES public.masterproduct(productid);


--
-- Name: transorderdetail transorderdetail_openpodetailproductid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transorderdetail
    ADD CONSTRAINT transorderdetail_openpodetailproductid_fkey FOREIGN KEY (openpodetailproductid) REFERENCES public.transopenpodetailproduct(openpodetailproductid);


--
-- Name: transorderdetail transorderdetail_orderheaderid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transorderdetail
    ADD CONSTRAINT transorderdetail_orderheaderid_fkey FOREIGN KEY (orderheaderid) REFERENCES public.transorderheader(orderheaderid);


--
-- Name: transorderheader transorderheader_openpodetaildropshipid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transorderheader
    ADD CONSTRAINT transorderheader_openpodetaildropshipid_fkey FOREIGN KEY (openpodetaildropshipid) REFERENCES public.transopenpodetaildropship(openpodetaildropshipid);


--
-- Name: transorderheader transorderheader_openpoheaderid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transorderheader
    ADD CONSTRAINT transorderheader_openpoheaderid_fkey FOREIGN KEY (openpoheaderid) REFERENCES public.transopenpoheader(openpoheaderid);


--
-- Name: transorderheader transorderheader_paymentmethodid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transorderheader
    ADD CONSTRAINT transorderheader_paymentmethodid_fkey FOREIGN KEY (paymentmethodid) REFERENCES public.masterpaymentmethod(paymentmethodid);


--
-- Name: transorderheader transorderheader_statustransorderid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transorderheader
    ADD CONSTRAINT transorderheader_statustransorderid_fkey FOREIGN KEY (statustransorderid) REFERENCES public.masterstatustransaksiorder(statustransorderid);


--
-- Name: transpembayaran transpembayaran_orderheaderid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transpembayaran
    ADD CONSTRAINT transpembayaran_orderheaderid_fkey FOREIGN KEY (orderheaderid) REFERENCES public.transorderheader(orderheaderid);


--
-- Name: transpengiriman transpengiriman_orderheaderid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transpengiriman
    ADD CONSTRAINT transpengiriman_orderheaderid_fkey FOREIGN KEY (orderheaderid) REFERENCES public.transorderheader(orderheaderid);


--
-- Name: transpengiriman transpengiriman_shipmentid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transpengiriman
    ADD CONSTRAINT transpengiriman_shipmentid_fkey FOREIGN KEY (shipmentid) REFERENCES public.mastershipment(shipmentid);


--
-- PostgreSQL database dump complete
--


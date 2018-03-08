--
-- PostgreSQL database dump
--

-- Dumped from database version 9.5.1
-- Dumped by pg_dump version 9.5.1

-- Started on 2016-05-27 12:34:34

SET statement_timeout = 0;
SET lock_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 1 (class 3079 OID 12355)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 2135 (class 0 OID 0)
-- Dependencies: 1
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET search_path = public, pg_catalog;

--
-- TOC entry 561 (class 1247 OID 839998)
-- Name: direction; Type: TYPE; Schema: public; Owner: admin
--

CREATE TYPE direction AS ENUM (
    'incoming',
    'outgoing'
);


ALTER TYPE direction OWNER TO admin;

--
-- TOC entry 198 (class 1255 OID 839968)
-- Name: return_boolean(boolean); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION return_boolean(p_boolean boolean) RETURNS boolean
    LANGUAGE plpgsql IMMUTABLE COST 1
    AS $$
        BEGIN
                IF p_boolean IS true THEN
			RETURN false;
		ELSE 
			RETURN true;
		END IF;
        END;
$$;


ALTER FUNCTION public.return_boolean(p_boolean boolean) OWNER TO postgres;

--
-- TOC entry 199 (class 1255 OID 839969)
-- Name: return_model(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION return_model(OUT p_id integer, OUT p_name text, OUT p_integer integer, OUT p_boolean boolean, OUT p_date timestamp with time zone) RETURNS record
    LANGUAGE plpgsql IMMUTABLE COST 1
    AS $$
        BEGIN
		p_id := 1;
                p_name := 'Test';
                p_integer := 23;
                p_date := now();
                p_boolean := true;
        END;
$$;


ALTER FUNCTION public.return_model(OUT p_id integer, OUT p_name text, OUT p_integer integer, OUT p_boolean boolean, OUT p_date timestamp with time zone) OWNER TO postgres;

--
-- TOC entry 200 (class 1255 OID 839970)
-- Name: return_number(integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION return_number(p_integer integer) RETURNS integer
    LANGUAGE plpgsql IMMUTABLE COST 1
    AS $$
        BEGIN
                RETURN p_integer + 1;
        END;
$$;


ALTER FUNCTION public.return_number(p_integer integer) OWNER TO postgres;

--
-- TOC entry 201 (class 1255 OID 839971)
-- Name: return_string(text, integer); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION return_string(p_string text, p_integer integer) RETURNS text
    LANGUAGE plpgsql IMMUTABLE COST 1
    AS $$
        BEGIN
                RETURN p_string || cast(p_integer as text);
        END;
$$;


ALTER FUNCTION public.return_string(p_string text, p_integer integer) OWNER TO postgres;

--
-- TOC entry 202 (class 1255 OID 839972)
-- Name: return_table(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION return_table() RETURNS TABLE(path text, section text, message text, created timestamp without time zone)
    LANGUAGE plpgsql IMMUTABLE COST 1
    AS $$
        BEGIN
		RETURN QUERY SELECT log.path, log.section, log.message, log.created FROM log;
        END;
$$;


ALTER FUNCTION public.return_table() OWNER TO postgres;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 185 (class 1259 OID 840003)
-- Name: enum; Type: TABLE; Schema: public; Owner: pgsql
--

CREATE TABLE enum (
    name text,
    direction direction NOT NULL
);


ALTER TABLE enum OWNER TO pgsql;

--
-- TOC entry 181 (class 1259 OID 839973)
-- Name: log; Type: TABLE; Schema: public; Owner: pgsql
--

CREATE TABLE log (
    id bigint NOT NULL,
    membership_id uuid,
    path text NOT NULL,
    section text NOT NULL,
    action integer NOT NULL,
    status integer NOT NULL,
    message text,
    created timestamp without time zone DEFAULT timezone('utc'::text, now()) NOT NULL,
    triggered_by text
);


ALTER TABLE log OWNER TO pgsql;

--
-- TOC entry 182 (class 1259 OID 839980)
-- Name: log_id_seq; Type: SEQUENCE; Schema: public; Owner: pgsql
--

CREATE SEQUENCE log_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE log_id_seq OWNER TO pgsql;

--
-- TOC entry 2138 (class 0 OID 0)
-- Dependencies: 182
-- Name: log_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: pgsql
--

ALTER SEQUENCE log_id_seq OWNED BY log.id;


--
-- TOC entry 183 (class 1259 OID 839982)
-- Name: sample; Type: TABLE; Schema: public; Owner: pgsql
--

CREATE TABLE sample (
    id integer NOT NULL,
    name text,
    age integer,
    is_valid boolean,
    created timestamp without time zone DEFAULT timezone('utc'::text, now()) NOT NULL
);


ALTER TABLE sample OWNER TO pgsql;

--
-- TOC entry 184 (class 1259 OID 839989)
-- Name: sample_id_seq; Type: SEQUENCE; Schema: public; Owner: pgsql
--

CREATE SEQUENCE sample_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE sample_id_seq OWNER TO pgsql;

--
-- TOC entry 2140 (class 0 OID 0)
-- Dependencies: 184
-- Name: sample_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: pgsql
--

ALTER SEQUENCE sample_id_seq OWNED BY sample.id;


--
-- TOC entry 2002 (class 2604 OID 839991)
-- Name: id; Type: DEFAULT; Schema: public; Owner: pgsql
--

ALTER TABLE ONLY log ALTER COLUMN id SET DEFAULT nextval('log_id_seq'::regclass);


--
-- TOC entry 2004 (class 2604 OID 839992)
-- Name: id; Type: DEFAULT; Schema: public; Owner: pgsql
--

ALTER TABLE ONLY sample ALTER COLUMN id SET DEFAULT nextval('sample_id_seq'::regclass);


--
-- TOC entry 2128 (class 0 OID 840003)
-- Dependencies: 185
-- Data for Name: enum; Type: TABLE DATA; Schema: public; Owner: pgsql
--

INSERT INTO enum (name, direction) VALUES ('RequestA', 'incoming');


--
-- TOC entry 2124 (class 0 OID 839973)
-- Dependencies: 181
-- Data for Name: log; Type: TABLE DATA; Schema: public; Owner: pgsql
--



--
-- TOC entry 2141 (class 0 OID 0)
-- Dependencies: 182
-- Name: log_id_seq; Type: SEQUENCE SET; Schema: public; Owner: pgsql
--

SELECT pg_catalog.setval('log_id_seq', 1, false);


--
-- TOC entry 2126 (class 0 OID 839982)
-- Dependencies: 183
-- Data for Name: sample; Type: TABLE DATA; Schema: public; Owner: pgsql
--

INSERT INTO sample (id, name, age, is_valid, created) VALUES (1, 'First', 23, true, '2016-05-25 15:54:56.665939');


--
-- TOC entry 2142 (class 0 OID 0)
-- Dependencies: 184
-- Name: sample_id_seq; Type: SEQUENCE SET; Schema: public; Owner: pgsql
--

SELECT pg_catalog.setval('sample_id_seq', 1, false);


--
-- TOC entry 2007 (class 2606 OID 839994)
-- Name: log_pkey; Type: CONSTRAINT; Schema: public; Owner: pgsql
--

ALTER TABLE ONLY log
    ADD CONSTRAINT log_pkey PRIMARY KEY (id);


--
-- TOC entry 2009 (class 2606 OID 839996)
-- Name: sample_pkey; Type: CONSTRAINT; Schema: public; Owner: pgsql
--

ALTER TABLE ONLY sample
    ADD CONSTRAINT sample_pkey PRIMARY KEY (id);


--
-- TOC entry 2134 (class 0 OID 0)
-- Dependencies: 7
-- Name: public; Type: ACL; Schema: -; Owner: admin
--

REVOKE ALL ON SCHEMA public FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM admin;
GRANT ALL ON SCHEMA public TO admin;
GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO PUBLIC;


--
-- TOC entry 2136 (class 0 OID 0)
-- Dependencies: 185
-- Name: enum; Type: ACL; Schema: public; Owner: pgsql
--

REVOKE ALL ON TABLE enum FROM PUBLIC;
REVOKE ALL ON TABLE enum FROM pgsql;
GRANT ALL ON TABLE enum TO pgsql;
GRANT ALL ON TABLE enum TO postgres;
GRANT ALL ON TABLE enum TO webuser_group;


--
-- TOC entry 2137 (class 0 OID 0)
-- Dependencies: 181
-- Name: log; Type: ACL; Schema: public; Owner: pgsql
--

REVOKE ALL ON TABLE log FROM PUBLIC;
REVOKE ALL ON TABLE log FROM pgsql;
GRANT ALL ON TABLE log TO pgsql;
GRANT ALL ON TABLE log TO postgres;
GRANT ALL ON TABLE log TO webuser_group;


--
-- TOC entry 2139 (class 0 OID 0)
-- Dependencies: 183
-- Name: sample; Type: ACL; Schema: public; Owner: pgsql
--

REVOKE ALL ON TABLE sample FROM PUBLIC;
REVOKE ALL ON TABLE sample FROM pgsql;
GRANT ALL ON TABLE sample TO pgsql;
GRANT ALL ON TABLE sample TO postgres;
GRANT ALL ON TABLE sample TO webuser_group;


-- Completed on 2016-05-27 12:34:34

--
-- PostgreSQL database dump complete
--


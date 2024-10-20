--
-- PostgreSQL database dump
--

-- Dumped from database version 16.4
-- Dumped by pg_dump version 16.4

-- Started on 2024-10-19 12:40:33

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

DROP DATABASE IF EXISTS svhdb;
--
-- TOC entry 4842 (class 1262 OID 16673)
-- Name: svhdb; Type: DATABASE; Schema: -; Owner: -
--

CREATE DATABASE svhdb WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';


\connect svhdb

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
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: -
--

CREATE SCHEMA public;


--
-- TOC entry 4843 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: -
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 216 (class 1259 OID 16747)
-- Name: Users; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."Users" (
    "Id" uuid NOT NULL,
    "UserName" text NOT NULL,
    "PasswordHash" text NOT NULL,
    "Email" text NOT NULL,
    "Hidden" boolean NOT NULL
);


--
-- TOC entry 215 (class 1259 OID 16674)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


--
-- TOC entry 222 (class 1259 OID 16787)
-- Name: documents; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.documents (
    did bigint NOT NULL,
    number character varying(50),
    docdate date NOT NULL,
    modecode character varying(5),
    size_doc integer NOT NULL,
    idmd5 character varying(32) NOT NULL,
    idsha256 character varying(64) NOT NULL,
    create_date timestamp with time zone DEFAULT now() NOT NULL,
    modify_date timestamp with time zone NOT NULL,
    pid bigint NOT NULL,
    docid uuid NOT NULL,
    doctype character varying(50)
);


--
-- TOC entry 223 (class 1259 OID 16811)
-- Name: documents_did_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.documents ALTER COLUMN did ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.documents_did_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 221 (class 1259 OID 16786)
-- Name: documents_size_doc_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.documents ALTER COLUMN size_doc ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.documents_size_doc_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 224 (class 1259 OID 16818)
-- Name: history_pkg; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.history_pkg (
    id uuid NOT NULL,
    "Pid" integer NOT NULL,
    oldst integer NOT NULL,
    newst integer NOT NULL,
    "StatusId" integer,
    comment text,
    create_date timestamp with time zone DEFAULT now() NOT NULL
);


--
-- TOC entry 220 (class 1259 OID 16769)
-- Name: packages; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.packages (
    pid bigint NOT NULL,
    create_date timestamp with time zone DEFAULT now() NOT NULL,
    modify_date timestamp with time zone NOT NULL,
    uuid uuid NOT NULL,
    user_id uuid NOT NULL,
    status integer DEFAULT 0 NOT NULL
);


--
-- TOC entry 219 (class 1259 OID 16768)
-- Name: packages_pid_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.packages ALTER COLUMN pid ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.packages_pid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 218 (class 1259 OID 16755)
-- Name: pkg_status; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public.pkg_status (
    id integer NOT NULL,
    stname character varying(250),
    runwf boolean DEFAULT false NOT NULL,
    mkres boolean DEFAULT false NOT NULL,
    sendmess boolean DEFAULT false NOT NULL
);


--
-- TOC entry 217 (class 1259 OID 16754)
-- Name: pkg_status_id_seq; Type: SEQUENCE; Schema: public; Owner: -
--

ALTER TABLE public.pkg_status ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.pkg_status_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 4828 (class 0 OID 16747)
-- Dependencies: 216
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 4827 (class 0 OID 16674)
-- Dependencies: 215
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20240918173112_InitUser', '8.0.8') ON CONFLICT DO NOTHING;
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20240923200750_UpdateClientDb', '8.0.8') ON CONFLICT DO NOTHING;
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20240929093224_UpdateClientDbAddHistory', '8.0.8') ON CONFLICT DO NOTHING;
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20240929160106_UpdateClientDbAddTypeDoc', '8.0.8') ON CONFLICT DO NOTHING;
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20241014165000_UpdateClientDbValueGenerated', '8.0.8') ON CONFLICT DO NOTHING;
INSERT INTO public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") VALUES ('20241016165718_UpdateClientDbTypeDocDate', '8.0.8') ON CONFLICT DO NOTHING;


--
-- TOC entry 4834 (class 0 OID 16787)
-- Dependencies: 222
-- Data for Name: documents; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 4836 (class 0 OID 16818)
-- Dependencies: 224
-- Data for Name: history_pkg; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 4832 (class 0 OID 16769)
-- Dependencies: 220
-- Data for Name: packages; Type: TABLE DATA; Schema: public; Owner: -
--



--
-- TOC entry 4830 (class 0 OID 16755)
-- Dependencies: 218
-- Data for Name: pkg_status; Type: TABLE DATA; Schema: public; Owner: -
--

INSERT INTO public.pkg_status (id, stname, runwf, mkres, sendmess) VALUES (0, 'Формируется', false, false, true) ON CONFLICT DO NOTHING;
INSERT INTO public.pkg_status (id, stname, runwf, mkres, sendmess) VALUES (1, 'Сформирован', false, false, true) ON CONFLICT DO NOTHING;
INSERT INTO public.pkg_status (id, stname, runwf, mkres, sendmess) VALUES (3, 'Принят', true, false, true) ON CONFLICT DO NOTHING;
INSERT INTO public.pkg_status (id, stname, runwf, mkres, sendmess) VALUES (4, 'Отклонен', true, true, true) ON CONFLICT DO NOTHING;
INSERT INTO public.pkg_status (id, stname, runwf, mkres, sendmess) VALUES (5, 'Отправлен', true, true, true) ON CONFLICT DO NOTHING;
INSERT INTO public.pkg_status (id, stname, runwf, mkres, sendmess) VALUES (99, 'Подтвержден', true, true, true) ON CONFLICT DO NOTHING;
INSERT INTO public.pkg_status (id, stname, runwf, mkres, sendmess) VALUES (107, 'Архивный', false, false, true) ON CONFLICT DO NOTHING;
INSERT INTO public.pkg_status (id, stname, runwf, mkres, sendmess) VALUES (217, 'Ожидает ответа архива', true, false, true) ON CONFLICT DO NOTHING;
INSERT INTO public.pkg_status (id, stname, runwf, mkres, sendmess) VALUES (214, 'Получен результат', true, true, true) ON CONFLICT DO NOTHING;
INSERT INTO public.pkg_status (id, stname, runwf, mkres, sendmess) VALUES (210, 'Отправка описи', true, false, true) ON CONFLICT DO NOTHING;
INSERT INTO public.pkg_status (id, stname, runwf, mkres, sendmess) VALUES (108, 'Ожидает ответа', true, false, true) ON CONFLICT DO NOTHING;
INSERT INTO public.pkg_status (id, stname, runwf, mkres, sendmess) VALUES (208, 'Размещение в архиве', true, false, true) ON CONFLICT DO NOTHING;


--
-- TOC entry 4844 (class 0 OID 0)
-- Dependencies: 223
-- Name: documents_did_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.documents_did_seq', 1, false);


--
-- TOC entry 4845 (class 0 OID 0)
-- Dependencies: 221
-- Name: documents_size_doc_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.documents_size_doc_seq', 1, false);


--
-- TOC entry 4846 (class 0 OID 0)
-- Dependencies: 219
-- Name: packages_pid_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.packages_pid_seq', 1, false);


--
-- TOC entry 4847 (class 0 OID 0)
-- Dependencies: 217
-- Name: pkg_status_id_seq; Type: SEQUENCE SET; Schema: public; Owner: -
--

SELECT pg_catalog.setval('public.pkg_status_id_seq', 1, false);


--
-- TOC entry 4667 (class 2606 OID 16753)
-- Name: Users PK_Users; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "PK_Users" PRIMARY KEY ("Id");


--
-- TOC entry 4665 (class 2606 OID 16678)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 4676 (class 2606 OID 16792)
-- Name: documents PK_documents; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.documents
    ADD CONSTRAINT "PK_documents" PRIMARY KEY (did);


--
-- TOC entry 4679 (class 2606 OID 16825)
-- Name: history_pkg PK_history_pkg; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.history_pkg
    ADD CONSTRAINT "PK_history_pkg" PRIMARY KEY (id);


--
-- TOC entry 4673 (class 2606 OID 16775)
-- Name: packages PK_packages; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.packages
    ADD CONSTRAINT "PK_packages" PRIMARY KEY (pid);


--
-- TOC entry 4669 (class 2606 OID 16762)
-- Name: pkg_status PK_pkg_status; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.pkg_status
    ADD CONSTRAINT "PK_pkg_status" PRIMARY KEY (id);


--
-- TOC entry 4674 (class 1259 OID 16812)
-- Name: IX_documents_pid; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_documents_pid" ON public.documents USING btree (pid);


--
-- TOC entry 4677 (class 1259 OID 16831)
-- Name: IX_history_pkg_StatusId; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_history_pkg_StatusId" ON public.history_pkg USING btree ("StatusId");


--
-- TOC entry 4670 (class 1259 OID 16804)
-- Name: IX_packages_status; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_packages_status" ON public.packages USING btree (status);


--
-- TOC entry 4671 (class 1259 OID 16805)
-- Name: IX_packages_user_id; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_packages_user_id" ON public.packages USING btree (user_id);


--
-- TOC entry 4682 (class 2606 OID 16813)
-- Name: documents FK_documents_packages_pid; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.documents
    ADD CONSTRAINT "FK_documents_packages_pid" FOREIGN KEY (pid) REFERENCES public.packages(pid) ON DELETE CASCADE;


--
-- TOC entry 4683 (class 2606 OID 16826)
-- Name: history_pkg FK_history_pkg_pkg_status_StatusId; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.history_pkg
    ADD CONSTRAINT "FK_history_pkg_pkg_status_StatusId" FOREIGN KEY ("StatusId") REFERENCES public.pkg_status(id);


--
-- TOC entry 4680 (class 2606 OID 16776)
-- Name: packages FK_packages_Users_user_id; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.packages
    ADD CONSTRAINT "FK_packages_Users_user_id" FOREIGN KEY (user_id) REFERENCES public."Users"("Id") ON DELETE CASCADE;


--
-- TOC entry 4681 (class 2606 OID 16781)
-- Name: packages FK_packages_pkg_status_status; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public.packages
    ADD CONSTRAINT "FK_packages_pkg_status_status" FOREIGN KEY (status) REFERENCES public.pkg_status(id) ON DELETE CASCADE;


-- Completed on 2024-10-19 12:40:34

--
-- PostgreSQL database dump complete
--


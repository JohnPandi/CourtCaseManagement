CREATE SCHEMA IF NOT EXISTS public;

DROP TABLE IF EXISTS public.process_responsible CASCADE;
DROP TABLE IF EXISTS public.responsible CASCADE;
DROP TABLE IF EXISTS public.situation CASCADE;
DROP TABLE IF EXISTS public.process CASCADE;

CREATE TABLE IF NOT EXISTS public.situation
(
    id uuid NOT NULL,
	name character varying(400) NULL,
	finished boolean NULL,
    CONSTRAINT situation_pkey PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public.responsible
(
    id uuid NOT NULL,
	cpf bigint NULL,
	name character varying(400) NULL,
	mail character varying(400) NULL,
	photograph text NULL,
    CONSTRAINT responsible_pkey PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public.process
(
    id uuid NOT NULL,
	process_id uuid NULL,
	situation_id uuid NULL,
	version int NULL,
	update_date Date NULL,
	update_user_name character varying(400) NULL,
	description character varying(1000) NULL,
	justice_secret boolean NULL,
	distribution_date Date NULL,
	client_physical_folder character varying(400) NULL,
	unified_process_number character varying(400) NULL,
    CONSTRAINT process_pkey PRIMARY KEY (id),
	CONSTRAINT process_id_fkey FOREIGN KEY (process_id) REFERENCES public.process (id),
	CONSTRAINT situation_id_fkey FOREIGN KEY (situation_id) REFERENCES public.situation (id)
);

CREATE TABLE IF NOT EXISTS public.process_responsible
(
    id uuid NOT NULL,
	process_id uuid NULL,
	responsible_id uuid NULL,
    CONSTRAINT process_responsible_pkey PRIMARY KEY (id),
	CONSTRAINT process_id_fkey FOREIGN KEY (process_id) REFERENCES public.process (id),
	CONSTRAINT responsible_id_fkey FOREIGN KEY (responsible_id) REFERENCES public.responsible (id)
);

Insert into public.situation (id, name, finished) values ('62a6ff9d-5f0d-407d-8e88-2bdfe66fa003', 'Em andamento', false);
Insert into public.situation (id, name, finished) values ('de66fe1c-05de-4eee-9fe9-3e0cb3aed09b', 'Desmembrado', false);
Insert into public.situation (id, name, finished) values ('b0cfc4bb-bee7-4aef-9fcb-b45e6500981b', 'Em recurso', false);
Insert into public.situation (id, name, finished) values ('4c0e22a2-b45c-4ea0-a61d-81b5ad15112b', 'Finalizado', true);
Insert into public.situation (id, name, finished) values ('79287b16-b102-4b29-904a-330450b7177d', 'Arquivado', true);

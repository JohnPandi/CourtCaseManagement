CREATE SCHEMA IF NOT EXISTS public;

DROP TABLE IF EXISTS public.responsible CASCADE;
DROP TABLE IF EXISTS public.situation CASCADE;
DROP TABLE IF EXISTS public.process CASCADE;

CREATE TABLE IF NOT EXISTS public.process
(
    id uuid NOT NULL,
	description character varying(400) NULL,
	justice_secret bit NULL,
	distribution_date Date NULL,
	client_physical_folder character varying(400) NULL,
	unified_process_number character varying(400) NULL,
    CONSTRAINT process_pkey PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS public.situation
(
    id uuid NOT NULL,
	name character varying(400) NULL,
	finished bit NULL,
	process_id uuid NOT NULL,
    CONSTRAINT situation_pkey PRIMARY KEY (id),
	CONSTRAINT process_id_fkey FOREIGN KEY (process_id) REFERENCES public.process (id)
);

CREATE TABLE IF NOT EXISTS public.responsible
(
    id uuid NOT NULL,
	cpf int NULL,
	name character varying(400) NULL,
	mail character varying(400) NULL,
	photograph LONGBLOB NULL,
	process_id uuid NOT NULL,
    CONSTRAINT stock_code_pkey PRIMARY KEY (id),
	CONSTRAINT process_id_fkey FOREIGN KEY (process_id) REFERENCES public.process (id)
);
ALTER TABLE "public"."dabe_project_person" DROP CONSTRAINT "FK_dabe_person_project_person_id";
ALTER TABLE "public"."dabe_project_person" DROP CONSTRAINT "FK_dabe_project_person_project_id";
DROP TABLE IF EXISTS "public"."dabe_person";
DROP TABLE IF EXISTS "public"."dabe_project";
DROP TABLE IF EXISTS "public"."dabe_project_person";
CREATE TABLE "public"."dabe_person" ( 
  "id" SERIAL,
  "person_name" VARCHAR(25) NOT NULL,
  CONSTRAINT "dabe_person_pkey" PRIMARY KEY ("id"),
  CONSTRAINT "UC_person_name" UNIQUE ("person_name")
);
CREATE TABLE "public"."dabe_project" ( 
  "id" SERIAL,
  "project_name" VARCHAR(50) NOT NULL,
  CONSTRAINT "dabe_project_pkey" PRIMARY KEY ("id"),
  CONSTRAINT "UC_project_name" UNIQUE ("project_name")
);
CREATE TABLE "public"."dabe_project_person" ( 
  "id" SERIAL,
  "project_id" INTEGER NOT NULL,
  "person_id" INTEGER NOT NULL,
  "hours" INTEGER NOT NULL,
  CONSTRAINT "dabe_project_person_pkey" PRIMARY KEY ("id")
);
ALTER TABLE "public"."dabe_project_person" ADD CONSTRAINT "FK_dabe_project_person_project_id" FOREIGN KEY ("project_id") REFERENCES "public"."dabe_project" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "public"."dabe_project_person" ADD CONSTRAINT "FK_dabe_person_project_person_id" FOREIGN KEY ("person_id") REFERENCES "public"."dabe_person" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
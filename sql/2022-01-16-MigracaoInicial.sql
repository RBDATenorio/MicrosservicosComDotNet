CREATE DATABASE "IdentidadeDb";

CREATE TABLE IF NOT EXISTS "Usuarios" 
(
	"Id" SERIAL PRIMARY KEY NOT NULL,
	"UserName" VARCHAR(100) NOT NULL,
	"Senha" VARCHAR(150) NOT NULL,
	"DataCadastro" DATE NOT NULL
);

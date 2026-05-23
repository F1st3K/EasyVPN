package repository

import (
	"database/sql"
	"log"

	_ "modernc.org/sqlite"
)

func NewDB(path string) *sql.DB {
	db, err := sql.Open("sqlite", path)
	if err != nil {
		log.Fatal(err)
	}

	if err := db.Ping(); err != nil {
		log.Fatal(err)
	}
	if err := InitDB(db); err != nil {
		log.Fatal(err)
	}
	return db
}

func InitDB(db *sql.DB) error {
	query := `
	CREATE TABLE IF NOT EXISTS connections (
		id TEXT PRIMARY KEY,
		uuid TEXT NOT NULL,
		enabled BOOLEAN NOT NULL
	);
	`

	_, err := db.Exec(query)
	return err
}

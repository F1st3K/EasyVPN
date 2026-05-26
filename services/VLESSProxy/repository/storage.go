package repository

import (
	"context"
	"database/sql"

	"github.com/google/uuid"
)

type SQLiteStorage struct {
	db *sql.DB
}

// init layer
func NewSQLiteStorage(db *sql.DB) *SQLiteStorage {
	return &SQLiteStorage{db: db}
}

// creating new disabled connection record
func (s *SQLiteStorage) CreateNewConnection(ctx context.Context, id string) error {

	newuuid, err := uuid.NewRandom()
	if err != nil {
		return err
	}

	query := `INSERT INTO connections (id,uuid,enabled) VALUES (?, ?, ?)`

	if _, err := s.db.ExecContext(ctx, query, id, newuuid, false); err != nil {
		return err
	}
	return nil
}

// list of all enabled conns for initing configs and reboot
func (s *SQLiteStorage) ListConnections(ctx context.Context) ([]string, error) {
	tx, err := s.db.BeginTx(ctx, &sql.TxOptions{})
	if err != nil {

		return nil, err
	}
	defer tx.Rollback()
	var res []string
	query := `SELECT uuid FROM connections WHERE enabled=1`
	rows, err := tx.Query(query)
	if err != nil {

		return nil, err
	}
	defer rows.Close()
	for rows.Next() {
		var uuid string

		if err := rows.Scan(&uuid); err != nil {
			return nil, err
		}
		res = append(res, uuid)
	}
	if err := rows.Err(); err != nil {
		return nil, err
	}
	return res, tx.Commit()
}

func (s *SQLiteStorage) GetUUID(ctx context.Context, id string) (string, error) {
	query := `SELECT uuid FROM connections WHERE id=?`

	row := s.db.QueryRowContext(ctx, query, id)
	if row.Err() != nil {
		return "", row.Err()
	}
	var res string
	err := row.Scan(&res)
	return res, err
}

func (s *SQLiteStorage) ActivateConnection(ctx context.Context, id string) error {
	query := `UPDATE connections SET enabled=1 WHERE id=?`

	res, err := s.db.ExecContext(ctx, query, id)
	if err != nil {
		return err
	}
	affected, err := res.RowsAffected()
	if err != nil {
		return err
	}

	if affected == 0 {
		return sql.ErrNoRows
	}
	return nil
}

func (s *SQLiteStorage) DeactivateConnection(ctx context.Context, id string) error {
	query := `UPDATE connections SET enabled=0 WHERE id=?`

	res, err := s.db.ExecContext(ctx, query, id)
	if err != nil {
		return err
	}
	affected, err := res.RowsAffected()
	if err != nil {
		return err
	}

	if affected == 0 {
		return sql.ErrNoRows
	}
	return nil
}

func (s *SQLiteStorage) DeleteConnection(ctx context.Context, id string) error {
	query := `DELETE FROM connections WHERE id=?`

	res, err := s.db.ExecContext(ctx, query, id)
	if err != nil {
		return err
	}
	affected, err := res.RowsAffected()
	if err != nil {
		return err
	}

	if affected == 0 {
		return sql.ErrNoRows
	}
	return nil
}

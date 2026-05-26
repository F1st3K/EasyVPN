package service

import (
	"VlessProxy/repository"
	"VlessProxy/singbox"
	"context"
)

type Service struct {
	orch *singbox.Orchestrator
	repo *repository.SQLiteStorage
}

func NewService(orch *singbox.Orchestrator,
	repo *repository.SQLiteStorage) *Service {
	return &Service{orch: orch, repo: repo}
}

func (s *Service) GetConfig(ctx context.Context, id string) (string, error) {
	uuid, err := s.repo.GetUUID(ctx, id)
	if err != nil {
		return "", err
	}
	return s.orch.CreateLink(uuid), nil
}

func (s *Service) CreateNewConnection(ctx context.Context, id string) error {
	return s.repo.CreateNewConnection(ctx, id)
}

func (s *Service) ActivateConnection(ctx context.Context, id string) error {
	if err := s.repo.ActivateConnection(ctx, id); err != nil {
		return err
	}
	uuids, err := s.repo.ListConnections(ctx)
	if err != nil {
		return err
	}

	if err := s.orch.UpdateConfigAndReload(uuids); err != nil {
		return err
	}
	return nil
}

func (s *Service) DeactivateConnection(ctx context.Context, id string) error {
	if err := s.repo.DeactivateConnection(ctx, id); err != nil {
		return err
	}
	uuids, err := s.repo.ListConnections(ctx)
	if err != nil {
		return err
	}

	if err := s.orch.UpdateConfigAndReload(uuids); err != nil {
		return err
	}
	return nil
}

func (s *Service) DeleteConnection(ctx context.Context, id string) error {
	if err := s.repo.DeleteConnection(ctx, id); err != nil {
		return err
	}
	uuids, err := s.repo.ListConnections(ctx)
	if err != nil {
		return err
	}

	if err := s.orch.UpdateConfigAndReload(uuids); err != nil {
		return err
	}
	return nil
}

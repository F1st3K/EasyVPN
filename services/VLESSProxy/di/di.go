package di

import (
	"VlessProxy/api"
	"VlessProxy/config"
	"VlessProxy/repository"
	"VlessProxy/service"
	"VlessProxy/singbox"
	"database/sql"
)

type DI struct {
	Cfg      *config.Config
	DB       *sql.DB
	Storage  *repository.SQLiteStorage
	Orch     *singbox.Orchestrator
	Service  *service.Service
	Handlers *api.Handler
}

func (d *DI) GetConfig() *config.Config {
	if d.Cfg != nil {
		return d.Cfg
	}
	Cfg, err := config.Load()
	if err != nil {
		panic(err)
	}
	d.Cfg = Cfg
	return d.Cfg
}

func (d *DI) GetDB() *sql.DB {
	if d.DB != nil {
		return d.DB
	}
	cfg := d.GetConfig()
	db := repository.NewDB(cfg.DBPath)
	d.DB = db
	return d.DB
}

func (d *DI) GetStorage() *repository.SQLiteStorage {
	if d.Storage != nil {
		return d.Storage
	}
	storage := repository.NewSQLiteStorage(d.GetDB())
	d.Storage = storage
	return d.Storage
}

func (d *DI) GetOrch() *singbox.Orchestrator {
	if d.Orch != nil {
		return d.Orch
	}
	cfg := d.GetConfig()
	orch := singbox.NewOrchestrator(cfg)
	d.Orch = orch
	return d.Orch
}

func (d *DI) GetService() *service.Service {
	if d.Service != nil {
		return d.Service
	}
	service := service.NewService(d.GetOrch(), d.GetStorage())
	d.Service = service
	return d.Service
}

func (d *DI) GetHandlers() *api.Handler {
	if d.Handlers != nil {
		return d.Handlers
	}
	cfg := d.GetConfig()
	auth := &api.Auth{Username: cfg.AdminUser, Password: cfg.AdminPassword}
	handlers := api.NewHandler(d.GetService(), auth)
	d.Handlers = handlers
	return d.Handlers
}

func (d *DI) Run() {
	cfg := d.GetConfig()
	handler := d.GetHandlers()
	routes := handler.InitRoutes()
	routes.Run(":" + cfg.Port)
}

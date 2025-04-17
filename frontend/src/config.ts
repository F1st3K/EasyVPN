interface AppConfig {
    ApiUrl: string;
    AuthCheckMinutes: number;
}

const defaultConfig: AppConfig = {
    ApiUrl: 'http://localhost:80/api/',
    AuthCheckMinutes: 15,
};

// Загрузка ENV переменных
const rawConfig = (window as any).APP_CONFIG || {};
const config: AppConfig = {
    ApiUrl:
        rawConfig.API_URL && !rawConfig.API_URL.includes('$')
            ? rawConfig.API_URL
            : defaultConfig.ApiUrl,

    AuthCheckMinutes:
        rawConfig.AUTH_CHECK_MINUTES && !rawConfig.AUTH_CHECK_MINUTES.includes('$')
            ? Number(rawConfig.AUTH_CHECK_MINUTES)
            : defaultConfig.AuthCheckMinutes,
};

export default config;

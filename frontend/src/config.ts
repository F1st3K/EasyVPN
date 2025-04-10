interface AppConfig {
    ApiUrl: string;
    AuthCheckMinutes: number;
}

const defaultConfig: AppConfig = {
    ApiUrl: 'http://localhost:80/api/',
    AuthCheckMinutes: 15,
};

// Загрузка ENV переменных
const config: AppConfig = {
    ApiUrl: process.env.REACT_APP_API_URL || defaultConfig.ApiUrl,
    AuthCheckMinutes:
        Number(process.env.REACT_APP_AUTH_CHECK_MINUTES) ||
        defaultConfig.AuthCheckMinutes,
};

export default config;

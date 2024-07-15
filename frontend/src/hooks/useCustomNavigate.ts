import { NavigateOptions, useNavigate } from 'react-router-dom';

export default function useCustomNavigate() {
    const navigate = useNavigate();

    const customNavigate = (to: string, options?: NavigateOptions) => {
        const isExternal = /^https?:\/\//.test(to);
        if (isExternal) {
            window.location.href = to;
        } else {
            navigate(to, options);
        }
    };

    return customNavigate;
}

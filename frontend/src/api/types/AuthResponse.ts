import User from "./User"

export default interface AuthResponse extends User {
    token: string
}
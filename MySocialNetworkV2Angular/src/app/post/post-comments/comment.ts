export interface Comment {
    id : number;
    date: Date;
    description: string;
    idPost: Number;
    user : string;
    UserImage: string;
}

export interface CommentCreation{
    description: string;
    date: Date;
    IdUser: number;
    IdPost: number;
}
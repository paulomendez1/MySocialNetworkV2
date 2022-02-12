export interface Post{
    Id: number;
    Description: string;
    Date: Date;
    Image: string;
    User: string;
    UserImage: string;
    Comments : number;
    Likes : number;
}

export interface PostCreation{
    description: string;
    date: Date;
    image: string;
    IdUser: number;
}
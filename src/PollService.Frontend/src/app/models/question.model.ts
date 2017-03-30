import { questionType } from "./question-type.model";

export class Question { 
    public id: any;
    public pollId: any;
    public name: string;
    public body: string;
    public description: string;
    public questionType: questionType = questionType.default;
}

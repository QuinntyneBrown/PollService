import { fetch } from "../utilities";
import { Question } from "./question.model";

export class QuestionService {
    constructor(private _fetch = fetch) { }

    private static _instance: QuestionService;

    public static get Instance() {
        this._instance = this._instance || new QuestionService();
        return this._instance;
    }

    public get(): Promise<Array<Question>> {
        return this._fetch({ url: "/api/question/get", authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { questions: Array<Question> }).questions;
        });
    }

    public getById(id): Promise<Question> {
        return this._fetch({ url: `/api/question/getbyid?id=${id}`, authRequired: true }).then((results:string) => {
            return (JSON.parse(results) as { question: Question }).question;
        });
    }

    public getBySurveyId(surveyid): Promise<Question> {
        return this._fetch({ url: `/api/question/getbysurveyid?surveyid=${surveyid}`, authRequired: true }).then((results: string) => {
            return (JSON.parse(results) as { questions: Array<Question> }).questions;
        });
    }
    public add(question) {
        return this._fetch({ url: `/api/question/add`, method: "POST", data: { question }, authRequired: true  });
    }

    public remove(options: { id : number }) {
        return this._fetch({ url: `/api/question/remove?id=${options.id}`, method: "DELETE", authRequired: true  });
    }
    
}

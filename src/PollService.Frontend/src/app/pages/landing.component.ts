import { ApiService } from "../shared";
import { createElement } from "../utilities";
import { Router } from "../router";

const template = require("./landing.component.html");
const styles = require("./landing.component.scss");

export class LandingComponent extends HTMLElement {
    constructor(
        private _apiService: ApiService = ApiService.Instance,
        private _router: Router = Router.Instance
    ) {
        super();
        this.onSubmit = this.onSubmit.bind(this);
    }
    
    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.survey = await this._apiService.getPoll();  
        for (let i = 0; i < this.survey.questions.length; i++) {
            let questionElement = createElement("<ce-question></ce-question>");
            questionElement.setAttribute("question", JSON.stringify(this.survey.questions[i]));
            this._questionsElement.appendChild(questionElement);
        }     
    }

    public survey: any;

    public async onSubmit() {
        var surveyResult = <any>{};

        surveyResult.responses = [];
        for (let i = 0; i < this._questionsElement.children.length; i++) {
            surveyResult.responses.push({
                value: (<HTMLInputElement>this._questionsElement.children[i]).value,
                questionId: (<any>this._questionsElement.children[i]).questionId
            });
        }

        await this._apiService.addPollResult(surveyResult);

        this._router.navigate(["thank-you"]);
    }

    private _setEventListeners() {
        this._submitElement.addEventListener("click", this.onSubmit);
    }

    disconnectedCallback() {
        this._submitElement.removeEventListener("click", this.onSubmit);
    }

    private responses: Array<any> = [];

    private get _questionsElement(): HTMLElement { return this.querySelector(".survey-questions") as HTMLElement; }
    private get _submitElement(): HTMLElement { return this.querySelector("ce-button") as HTMLElement; }
}

customElements.define(`ce-landing`,LandingComponent);
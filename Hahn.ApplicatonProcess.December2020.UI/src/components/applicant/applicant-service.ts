import { HttpClient, HttpRequestMessage } from 'aurelia-http-client';
import { Applicant } from './models/applicant';
import { autoinject } from 'aurelia-framework';
import { AureliaConfiguration  } from 'aurelia-configuration';


@autoinject
export class ApplicantService {
    constructor(private httpClient: HttpClient,private config:AureliaConfiguration) {
        this.httpClient.configure(x => {
            x.withHeader('Content-Type','application/json')
        });
    }
    sendApplicantData(applicant: Applicant) {
        return this.httpClient.post(this.config.get('apiHost')+this.config.get('applicantApiSection'),applicant);
    }
    async validCountry(con:string) : Promise<boolean>{
        let result = false;
        await this.httpClient.jsonp(this.config.get('countryValidationUrl')+con).then(suc=>{
            result= true;
        }).catch(err=>{
            result=false;
        });
        return result;
    }
}
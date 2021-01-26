import { NotificationService } from './../../services/notification.service';
import { DialogComponent } from './../dialog-component/dialog.component';
import { Dialog } from './../../utils/dialog';
import { Applicant } from './../../models/applicant';
import { ApplicantService } from './../../services/applicant.service';
import { autoinject, bindable } from "aurelia-framework";
import { EventAggregator } from "aurelia-event-aggregator";
import { Router } from "aurelia-router";
import { DialogService } from "aurelia-dialog";
import { I18N } from "aurelia-i18n";

@autoinject()
export class ApplicantList {
  private router: Router;
  private dlg: DialogService;
  private ea: EventAggregator;
  title: string;
  applicants: Applicant[];

  searchBy: number;
  selectedId: number;

  @bindable
  msg = "Are you sure";

  constructor(
    router: Router,
    private applicantService: ApplicantService,
    dialogService: DialogService,  
    private notificationService: NotificationService,
    ea: EventAggregator) {
    this.router = router;
    this.dlg = dialogService;
    this.ea = ea;
    this.title = "Applicant List";
  }

  // @bindable
  // async action(id: number) {
  //   await this.applicantService.deleteApplicant(id).then(async (response) => {
  //     if (response.statusCode != 201) {
  //       alert("Something went wrong!!!");
  //     }
  //     await this.search();
  //   });
  //   return true;
  // }

  
  activate() {
    try {
      this.search();
    } catch (error) {
      console.log(error);
    }
  }

  search(): boolean {
    this.applicantService
      .getApplicantList()
      .then((response) => (this.applicants = response));
    console.log(this.applicants);
    return true;
  }

  async searchById(id) {
    await this.applicantService
      .getApplicantById(id)
      .then((response) => {
        this.applicants = [];
        this.applicants.push(response);
      });
    console.log(this.applicants);
    return true;
  }

  removeApplicant(id: number) {

    this.notificationService.confirm().then((value) => {
      if(value == true){
        this.applicantService.deleteApplicant(id).then(() => {
          this.search();
        });
      }
    });

    // this.dlg
    //   .open({
    //     viewModel: DialogComponent,
    //     model: this.msg,
    //   })
    //   .then(async (result) => {
    //     if (!result.wasCancelled) {
          
      //   } else {
      //     console.log("cancelled");
      //   }
      // });
  }
}

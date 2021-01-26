import { NotificationService } from './../../services/notification.service';
import { DialogComponent } from './../dialog-component/dialog.component';
import { CommonService } from './../../services/common.service';
import { ApplicantService } from './../../services/applicant.service';
import { BootstrapFormRenderer } from "./../../utils/bootstrap-form-renderer";
import { Applicant } from "./../../models/applicant";
import * as swal from 'sweetalert';
import {
  inject,
  CompositionTransaction,
  CompositionTransactionNotifier,
} from "aurelia-framework";
import { Router } from "aurelia-router";
import {
  ValidationControllerFactory,
  ValidationRules,
} from "aurelia-validation";
import { EventAggregator } from "aurelia-event-aggregator";
import { DialogService } from "aurelia-dialog";
import { I18N } from "aurelia-i18n";

@inject(
  ApplicantService,
  CommonService,
  ValidationControllerFactory,
  CompositionTransaction,
  Router,
  I18N,
  DialogService,
  EventAggregator,
  NotificationService
)
export class InsertApplicantComponent {
  // private applicantervice: ApplicantService;
  // private commonService: CommonService;
  // private controllerFactory: ValidationControllerFactory;
  private notifier: CompositionTransactionNotifier;
  // private router: Router;
  // private i18N: I18N;
  // private dialogService: DialogService;
  // private ea: EventAggregator;
  controller = null;
  title: string;
  validation: string;
  standardGetMessage: string;
  applicant: Applicant;

  constructor(
    private applicantervice: ApplicantService,
    private commonService: CommonService,
    private controllerFactory: ValidationControllerFactory,
    private compositionTransaction: CompositionTransaction,
    private router: Router,
    private i18N: I18N,
    private dialogService: DialogService,
    private ea: EventAggregator,
    private notificationService: NotificationService
  ) {
    this.controller = controllerFactory.createForCurrentScope();
    this.controller.addRenderer(new BootstrapFormRenderer());
    this.notifier = compositionTransaction.enlist();
    this.router = router;
    this.title = "Add New Applicant";
    this.i18N = i18N;
    this.dialogService = dialogService;
    this.ea = ea;
    this.applicant = {} as Applicant;
  }
  attached(): void {
    // do nothing for now
  }
  action(): void {
    this.clearData();
  }

  openDialog(): void {
    this.notificationService.confirm().then((response) => {
      if(response == true)
      {
        this.action();
      }
    });
    // swal({
    //   title: 'Are you sure?',
    //   // text: "Are you sure?",
    //   icon: 'warning',
    //   buttons: {yes: 'Yes', no: 'No'}
    //   // dangerMode: true,
    // })
    // .then((value) => {
    //   if (value == 'yes') {
    //     // swal("Poof! Your imaginary file has been deleted!", {
    //       // icon: "success",
    //     // });

    //     this.action();

    //   } else {
    //     // swal("Your imaginary file is safe!");
    //   }
    // });

    // // this.dialogService
    // //   .open({
    // //     viewModel: DialogComponent,
    // //     model: "are you really sure to reset all the data",
    // //   })
    // //   .whenClosed()
    // //   .then((respose) => {
    // //     console.log(respose);
    // //     this.action();
    // //   });
  }

  isChecked(value: boolean): void {
    this.applicant.hired = value;
  }

  clearData(): void {
    this.applicant = null;
  }

  //enable send button when form validation is done
  get canSave(): boolean {
    
    const isValid = this.applicant.name &&
    this.applicant.familyName  &&
    this.applicant.address  &&
    this.applicant.countryOfOrigin  &&
    this.applicant.emailAddress  &&
    this.applicant.age >= 20 &&
    this.applicant.age <= 60;
    
    return (isValid);
  }

  //enable reset button when user type something
  get canReset(): boolean {
    return (
      typeof(this.applicant.name) != 'undefined' ||
      typeof(this.applicant.familyName) != 'undefined' ||
      typeof(this.applicant.address) != 'undefined' ||
      typeof(this.applicant.countryOfOrigin) != 'undefined' ||
      typeof(this.applicant.emailAddress) != 'undefined' ||
      typeof(this.applicant.age) != 'undefined'
    );
  }

  async searchById(id: number): Promise<void> {
    await this.applicantervice
      .getApplicantById(id)
      .then((response) => (this.applicant = response));
  }

  checkValidcountry(countryName: string): number {
    let valid = 0;

    try {
      const data = JSON.parse(localStorage.getItem("valid-countries"));
      

      if (data.length > 0) {
        for (let i = 0; i < data.length; i++) {
          const cName = data[i];

          if (
            cName.name.toLowerCase() == countryName.toLowerCase() ||
            cName.name.toUpperCase() == countryName.toUpperCase()
          ) {
            valid = 1;
            break;
          }
        }
      }
    } catch (error) {
      console.log(error);
    }
    return valid;
  }

  async activate(): Promise<void> {
    try {
      if (localStorage.getItem("countries") === null) {
        await this.commonService.getCountry();
      }
      this.notifier.done();
      await this.setupValidation();
    } catch (error) {
      console.log(error);
    }
  }

  setupValidation(): void {
    //Custom validation for checking between two numbers
    ValidationRules.customRule(
      "integerRange",
      (value, obj, min, max) => {
        const num = Number.parseInt(value);
        return (
          num === null ||
          num === undefined ||
          (Number.isInteger(num) && num >= min && num <= max)
        );
      },
      "${$displayName} must be an integer between ${$config.min} and ${$config.max}.",
      (min, max) => ({ min, max })
    );

    //validation rules starts from here
    ValidationRules.ensure("name")
      .displayName("name")
      .required()
      .minLength(5)
      .withMessage("Name at least 5 Characters")

      .ensure("familyName")
      .displayName("familyName")
      .required()
      .minLength(5)
      .withMessage("FamilyName - at least 5 Characters")

      .ensure("address")
      .required()
      .minLength(10)
      .withMessage("Address - at least 10 Characters")

      .ensure("countryOfOrigin")
      .required()
      .withMessage("Please select country")

      .ensure("emailAddress")
      .required()
      .email()
      .withMessage("EmailAdress is required")

      .ensure("age")
      .required()
      .satisfiesRule("integerRange", 20, 60)
      .withMessage("Age – must be between 20 and 60")

      .on(this.applicant);
  }

  //this function will fire when user click the submit button
  async formSubmit(): Promise<void> {
    try {
      const res = await this.controller.validate();

      if (res.valid) {
        this.create();
      }
    } catch (error) {
      console.log(error);
    }
  }

  async create(): Promise<void> {
    try {
      const result = this.checkValidcountry(this.applicant.countryOfOrigin);

      if (result == 1) {
        const v = this.applicant.age.toString();
        this.applicant.age = parseInt(v);

        await this.applicantervice
          .insertApplicant(this.applicant)
          .then((response) => {
            if (response == undefined) {
              this.notificationService.alert(
                "Something went wrong. Please check your internet connection..."
              );
            } else if (response.statusCode == 201) {
              this.clearData();
              this.notificationService.success(null, 'Applicant saved successfully').then(() => {
                this.router.navigateToRoute("list");
              });
            } else {
              this.notificationService.alert("Something went wrong!!!");
            }
          });
      } else {
        this.notificationService.danger("Country not found");
      }
    } catch (error) {
      console.log(error);
    }
  }
}

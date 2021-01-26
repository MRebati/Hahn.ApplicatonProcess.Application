import { CommonService } from './../../services/common.service';
import { NotificationService } from './../../services/notification.service';
import { Dialog } from './../../utils/dialog';
import { Applicant } from './../../models/applicant';
import { ApplicantService } from './../../services/applicant.service';
import {
  inject,
  autoinject,
  bindable,
  CompositionTransaction,
  CompositionTransactionNotifier,
} from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';
import { Router } from 'aurelia-router';
import { I18N } from 'aurelia-i18n';
import {
  ValidationControllerFactory,
  ValidationRules,
} from 'aurelia-validation';
import { BootstrapFormRenderer } from 'utils/bootstrap-form-renderer';

@autoinject()
export class UpdateApplicantComponent {
  notifier: CompositionTransactionNotifier;
  controller = null;
  title: any;
  validation: any;
  standardGetMessage: any;
  applicant: Applicant = {} as Applicant;
  data: any;

  constructor(
    private applicantervice: ApplicantService,
    private commonService: CommonService,
    controllerFactory: ValidationControllerFactory,
    compositionTransaction: CompositionTransaction,
    private router: Router,
    private i18N: I18N,
    private ea: EventAggregator,
    private notificationService: NotificationService,
    app: Applicant
  ) {
    this.applicantervice = applicantervice;
    this.controller = controllerFactory.createForCurrentScope();
    this.controller.addRenderer(new BootstrapFormRenderer());
    this.notifier = compositionTransaction.enlist();
    this.router = router;
    this.title = 'Update Applicant';
    this.i18N = i18N;
    this.ea = ea;
    this.applicant = app;
  }
  attached(): void {
    // do nothing
  }
  action(): void {
    this.clearData();
  }

  public openDialog() {
    this.notificationService.confirm().then((value) => {
      if (value == true) {
        this.action();
      }
    });
    // this.dialogService
    //   .open({
    //     viewModel: Dialog,
    //     model: 'are you really sure to reset all the data',
    //   })
    //   .whenClosed()
    //   .then((respose) => {
    //     this.action();
    //   });
  }

  isChecked(value) {
    this.applicant.hired = value;
  }

  clearData() {
    this.applicant = null;
    this.router.navigateToRoute('ApplicantList');
  }

  //enable send button when form validation is done
  get canSave() {
    return (
      this.applicant.name &&
      this.applicant.familyName &&
      this.applicant.address &&
      this.applicant.countryOfOrigin &&
      this.applicant.emailAddress &&
      this.applicant.age >= 20 &&
      this.applicant.age <= 60
    );
  }

  async searchById(id) {
    await this.applicantervice
      .getApplicantById(id)
      .then(
        (response) => (this.applicant = response));
    console.log(this.applicant);
  }

  checkValidcountry(countryName): boolean {
    let valid = false;
    try {
      const data = JSON.parse(localStorage.getItem('valid-countries'));

      if (data.length > 0) {
        for (let i = 0; i < data.length; i++) {
          const cName = data[i];
          console.log(cName);
          if (
            cName.name.toLowerCase() == countryName.toLowerCase() ||
            cName.name.toUpperCase() == countryName.toUpperCase()
          ) {
            valid = true;
            break;
          }
        }
      }
    } catch (error) {
      console.log(error);
    }
    return valid;
  }

  activate = async (params) => {
    try {
      if (localStorage.getItem('countries') === null) {
        await this.commonService.getCountry();
      }
      await this.searchById(params.id);
      this.notifier.done();
      this.setupValidation();
    } catch (error) {
      console.log(error);
    }
  };

  setupValidation() {
    //Custom validation for checking between two numbers
    ValidationRules.customRule(
      'integerRange',
      (value, obj, min, max) => {
        const num = Number.parseInt(value);
        return (
          num === null ||
          num === undefined ||
          (Number.isInteger(num) && num >= min && num <= max)
        );
      },
      '${$displayName} must be an integer between ${$config.min} and ${$config.max}.',
      (min, max) => ({ min, max })
    );

    //validation rules starts from here
    ValidationRules.ensure('name')
      .displayName('name')
      .required()
      .minLength(5)
      .withMessage('Name at least 5 Characters')

      .ensure('familyName')
      .displayName('familyName')
      .required()
      .minLength(5)
      .withMessage('FamilyName - at least 5 Characters')

      .ensure('address')
      .required()
      .minLength(10)
      .withMessage('Address - at least 10 Characters')

      .ensure('countryOfOrigin')
      .required()
      .withMessage('Please select country')

      .ensure('emailAdress')
      .required()
      .email()
      //.matches(RegExp('^(\\w+.*)(\\@)(\\w+.*)$', 'gi'))
      .withMessage('EmailAdress is required')

      .ensure('age')
      .required()
      .satisfiesRule('integerRange', 20, 60)
      .withMessage('Age – must be between 20 and 60')

      .on(this.applicant);
  }

  //this function will fire when user click the submit button
  execute = async () => {
    try {
      const res = await this.controller.validate();

      if (res.valid) {
        this.update();
      }
    } catch (error) {
      console.log(error);
    }
  };

  update = async () => {
    const result = this.checkValidcountry(this.applicant.countryOfOrigin);
    try {
      if (result) {
        const v = this.applicant.age.toString();
        this.applicant.age = parseInt(v);
        try {
          await this.applicantervice
            .updateApplicant(this.applicant)
            .then((response) => {
              if (response.statusCode == 201) {
                this.notificationService.success(null, 'Updating Applicant was successful!').then(() => {
                  this.router.navigateToRoute('list');
                })
              } else {
                this.notificationService.alert('Something went wrong!!!');
              }
            });
        } catch (error) {
          console.log(error);
        }
      } else {
        this.notificationService.alert('Country not found');
      }
    } catch (error) {
      console.log(error);
    }
  };
}

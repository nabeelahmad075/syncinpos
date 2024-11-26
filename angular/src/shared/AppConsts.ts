import { SelectItem } from "primeng/api";
import { Genders } from "./service-proxies/service-proxies";

export class AppConsts {

    static readonly tenancyNamePlaceHolderInUrl = '{TENANCY_NAME}';

    static remoteServiceBaseUrl: string;
    static appBaseUrl: string;
    static appBaseHref: string; // returns angular's base-href parameter value if used during the publish

    static localeMappings: any = [];

    static readonly userManagement = {
        defaultAdminUserName: 'admin'
    };

    static readonly localization = {
        defaultLocalizationSourceName: 'syncinpos'
    };

    static readonly authorization = {
        encryptedAuthTokenName: 'enc_auth_token'
    };

    static readonly genderList: SelectItem[] = [
        {
          label: 'Male',
          value: Genders._1
        },
        {
          label: 'Female',
          value: Genders._2
        }
      ]
}

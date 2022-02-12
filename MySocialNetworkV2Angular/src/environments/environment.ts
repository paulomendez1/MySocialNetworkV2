// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  apiURL: "https://localhost:44315/api",
  openIdConnectSettings: {
    authority: 'https://localhost:44380',
    client_id: "mysocialnetworkclient",
    client_secret: 'secret',
    grant_type : 'client_credentials',
    redirect_uri: 'https://localhost:4200/signin-oidc',
    scope: 'openid profile mysocialnetworkapi.read mysocialnetworkapi.write',
    response_type: 'id_token token',
    post_logout_redirect_uri: 'https://localhost:4200/',
    automaticSilentRenew: true,
    silent_redirect_uri: 'https://localhost:4200/redirect-silentrenew'
  }
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.

﻿/// <reference path="../model.ts" />

module AutomatedTesting {
    'use strict';

    export interface IAccountsService {
        getClientAccounts(clientId: number): ng.IPromise<Account[]>;
        getClientTotalAccountBalance(clientId: number): ng.IPromise<number>;
    }

    class AccountsService {

        private baseUrl: string;

        public static $inject = [
            "$log",
            "$http"
        ];

        constructor(
            private $log: ng.ILogService,
            private $http: ng.IHttpService
            ) {
            this.baseUrl = 'http://localhost:21895/AccountsService.svc';
        }

        getClientAccounts(clientId: number): ng.IPromise<Account[]> {
            this.$log.debug('Retrieving accounts for client ' + clientId);

            return this.$http.get([this.baseUrl, 'clients', clientId, 'accounts'].join('/'))
                .then(resp => resp.data);
        }

        getClientTotalAccountBalance(clientId: number): ng.IPromise<number> {
            this.$log.debug('Retrieving total balance for client ' + clientId);

            return this.$http.get([this.baseUrl, 'clients', clientId, 'accounts', 'balance'].join('/'))
                .then(resp => resp.data);
        }

    }


    angular.module('AutomatedTestingApp')
        .service('AccountsService', AccountsService);
}
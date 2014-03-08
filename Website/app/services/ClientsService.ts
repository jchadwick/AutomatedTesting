/// <reference path="../model.ts" />

module AutomatedTesting {
    'use strict';

    export interface IClientsService {
        getClient(clientId: number): ng.IPromise<Client>;
        getClients(): ng.IPromise<Client[]>;
    }

    class ClientsService {

        private baseUrl: string;

        public static $inject = [
            "$log",
            "$http"
        ];

        constructor(
            private $log: ng.ILogService,
            private $http: ng.IHttpService
            ) {
            this.baseUrl = 'http://localhost:21895/ClientsService.svc';
        }

        getClient(clientId: number): ng.IPromise<Client> {
            this.$log.debug('Retrieving Client #' + clientId);

            return this.$http.get([this.baseUrl, 'clients', clientId, 'Clients'].join('/'))
                .then(resp => resp.data);
        }

        getClients(): ng.IPromise<Client[]> {
            this.$log.debug('Retrieving Clients');

            return this.$http.get([this.baseUrl, 'clients'].join('/'))
                .then(resp => resp.data);
        }

    }


    angular.module('AutomatedTestingApp')
        .service('ClientsService', ClientsService);
}
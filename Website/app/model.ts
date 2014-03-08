module AutomatedTesting {
    'use strict';

    export enum AccountType {
        Undefined,
        Checking,
        Savings,
        Brokerage,
        Retirement,
    }

    export interface Account {
        Id: number;
        ClientId: number;
        Name: string;
        Type: AccountType;
        Balance: number;
    }

    export interface Client {
        Id: number;
        Name: string;
    }

}
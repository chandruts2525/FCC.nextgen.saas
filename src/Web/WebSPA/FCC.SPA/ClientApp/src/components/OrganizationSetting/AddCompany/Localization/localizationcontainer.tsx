import { useState } from "react";
import Localization from "./localization";
import moment from 'moment';
import React from "react";

const LocalizationContainer = ({ collectData, formData }: any) => {

    const measurementTypeOptions = [
        { value: 'Imperial', label: 'Imperial' },
        { value: 'External', label: 'External' }
    ];

    const languageOptions = [
        { value: 'English', label: 'English' },
        { value: 'Japanese', label: 'Japanese' }
    ];

    const currencyOptions = [
        { value: '$USD', label: '$USD' },
        { value: 'Indian Rupee', label: 'Indian Rupee' }
    ];

    const timeZoneOptions = [
        { value: '$US/Michigan (-5: 00 EST)', label: '$US/Michigan (-5: 00 EST)' },
        { value: 'Asia/Kolkata UTC+5:30', label: 'Asia/Kolkata UTC+5:30' }
    ];

    const dateFormatOptions = [
        { value: 'DD/MM/YYYY', label: 'DD/MM/YYYY' },
        { value: 'MM/DD/YYYY', label: 'MM/DD/YYYY' },
        { value: 'YYYY/MM/DD', label: 'YYYY/MM/DD' }
    ];

    const timeFormatOptions = [
        { value: 'HH:MM:ss', label: 'HH:MM:ss' },
        { value: 'ss:MM:HH', label: 'ss:MM:HH' }
    ];

    return (
        <Localization
            measurementTypeOptions={measurementTypeOptions}
            currencyOptions={currencyOptions}
            languageOptions={languageOptions}
            timeZoneOptions={timeZoneOptions}
            dateFormatOptions={dateFormatOptions}
            timeFormatOptions={timeFormatOptions}
            collectData={collectData}
            formData={formData}
        />
    );
};

export default LocalizationContainer;
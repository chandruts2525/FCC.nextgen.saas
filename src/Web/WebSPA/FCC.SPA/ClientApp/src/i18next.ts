// i18n.ts
import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import translationTxt from './utils/translation.json';
const resources = {
  language: {
    translation: translationTxt,
  },
};

i18n.use(initReactI18next).init({
  resources,
  lng: 'language',
  fallbackLng: 'language',
  interpolation: {
    escapeValue: false,
  },
});

export default i18n;

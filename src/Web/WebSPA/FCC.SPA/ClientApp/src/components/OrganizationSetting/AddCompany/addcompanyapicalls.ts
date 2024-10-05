import ApiConfig from '../../../api/ApiConfig';

export async function callAddCompanyApi(data: any) {
  const response = await ApiConfig.post(data);
  return response;
}

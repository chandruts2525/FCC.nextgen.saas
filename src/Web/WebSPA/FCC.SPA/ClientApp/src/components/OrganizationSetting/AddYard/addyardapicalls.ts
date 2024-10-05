import ApiConfig from '../../../api/ApiConfig';

export async function callAddYardApi(data: any) {
  const response = await ApiConfig.post(data);
  return response;
}

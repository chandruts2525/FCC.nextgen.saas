import ApiConfig from '../../api/ApiConfig';

export async function callOrganizationApi(data: any) {
  const response = await ApiConfig.post(data);
  return response;
}

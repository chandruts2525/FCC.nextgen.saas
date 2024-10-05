import { dataURLFileLoader } from "@cyntler/react-doc-viewer";
import ApiConfig from "../../api/ApiConfig";

// export async function getRoleList(url: string, param?: any) {
//   const response = await ApiConfig.get(url, param);
//   return response;
// }

// export async function getRoleAttachment(url: string, param?: any) {
//   const response = await ApiConfig.get(url, param);
//   return response;
// }

// export async function addRole(url: string, data: any) {
//   const response = await ApiConfig.post(url, data);
//   return response;
// }

// export async function addAttachment(url: string, data: any) {
//   const response = await ApiConfig.post(url, data);
//   return response;
// }

// export async function updateRole(url: string, data: any) {
//   const response = await ApiConfig.put(url, data);
//   return response;
// }

// export async function editRole(url: string, data: any) {
//   const response = await ApiConfig.put(url, data);
//   return response?.data?.data;
// }

// export async function deleteRole(url: string, param?: any) {
//   const response = await ApiConfig.delete(url, param);
//   return response?.data?.data;
// }

// export async function deleteAttachment(url: string, param?: any) {
//   const response = await ApiConfig.delete(url, param);
//   return response;
// }

// export async function exportRole(url: string) {
//   const response = await ApiConfig.get(url);
//   return response;
// }

export async function callRoleApi(data: any, isUpload = false) {
  const response = await ApiConfig.post(data, isUpload);
  return response;
}

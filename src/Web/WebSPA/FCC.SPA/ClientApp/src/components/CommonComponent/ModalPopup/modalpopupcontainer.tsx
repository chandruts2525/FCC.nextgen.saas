import React from 'react';
import ModalPopup from './modalpopups';

interface IProps {
  closeModalPopup?: any,
  ModalPopupTitle?: string,
  ModalPopupType?: string,
  ShowModalPopupFooter?: any,
  FooterSecondaryBtnTxt?: any,
  FooterPrimaryBtnTxt?: any,
  FooterActionBtnTxt?:any,
  children?: any,
  ModalPopupName?:any,
  PrimaryBtnOnclick?:any,
  SecondryBtnOnclick?:any,
  ActionBtnOnclick?:any,
  FooterActionPrimaryyBtnTxt?:any,
  PrimaryActionBtnOnclick?:any
  disablePrimaryActionBtn?: boolean
}
const ModalPopupContainer = ({
  closeModalPopup,
  ModalPopupTitle,
  ModalPopupType,
  ShowModalPopupFooter,
  FooterSecondaryBtnTxt,
  FooterPrimaryBtnTxt,
  FooterActionBtnTxt,
  children,
  ModalPopupName,
  PrimaryBtnOnclick,
  SecondryBtnOnclick,
  ActionBtnOnclick,
  FooterActionPrimaryyBtnTxt,
  PrimaryActionBtnOnclick,
  disablePrimaryActionBtn
}:IProps) => {
  return (
      <ModalPopup ModalPopupName ={ModalPopupName} ShowModalPopupFooter = {ShowModalPopupFooter} FooterSecondaryBtnTxt = {FooterSecondaryBtnTxt} FooterActionBtnTxt={FooterActionBtnTxt} ModalPopupType = {ModalPopupType} FooterPrimaryBtnTxt = {FooterPrimaryBtnTxt} closeModalPopup={closeModalPopup} ModalPopupTitle={ModalPopupTitle} PrimaryBtnOnclick={PrimaryBtnOnclick} SecondryBtnOnclick={SecondryBtnOnclick} ActionBtnOnclick={ActionBtnOnclick} FooterActionPrimaryyBtnTxt={FooterActionPrimaryyBtnTxt} PrimaryActionBtnOnclick={PrimaryActionBtnOnclick} disablePrimaryActionBtn={disablePrimaryActionBtn}>{children}</ModalPopup>
  )
}

export default ModalPopupContainer

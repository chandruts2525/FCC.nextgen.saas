import React, { useEffect } from 'react'
import { PrimaryButton, OutlineButton } from '..'
import {CloseIcon} from '../../../assets/images';
import './modalpopup.scss'

interface IProps {
  closeModalPopup?: any,
  ModalPopupTitle?: string,
  ModalPopupType?: string,
  ShowModalPopupFooter?: any,
  FooterSecondaryBtnTxt?: any,
  FooterActionBtnTxt?:any,
  FooterPrimaryBtnTxt?: any,
  ModalPopupName?: string,
  children?: any,
  PrimaryBtnOnclick?:any,
  SecondryBtnOnclick?:any,
  ActionBtnOnclick?:any,
  FooterActionPrimaryyBtnTxt?:any,
  PrimaryActionBtnOnclick?:any,
  disablePrimaryActionBtn?: boolean
}
const ModalPopupInner = ({
  ModalPopupType,
  ModalPopupTitle,
  closeModalPopup,
  ShowModalPopupFooter,
  FooterSecondaryBtnTxt,
  FooterActionBtnTxt,
  FooterPrimaryBtnTxt,
  ModalPopupName,
  children,
  PrimaryBtnOnclick,
  SecondryBtnOnclick,
  ActionBtnOnclick,
  FooterActionPrimaryyBtnTxt,
  PrimaryActionBtnOnclick,
  disablePrimaryActionBtn
}:IProps) => {
  useEffect(() => {
    document.body.classList.add('modal-open')
    return () => {
      document.body.classList.remove('modal-open')
    }
  }, [])
  return (
        <div className={`modalPopupMasterContainer ${ModalPopupName}`}>
            <div className='dropShadowcontainer'></div>
            <div className={`modalPopupContainer ${ModalPopupType}`}>
              <div className='clearfix'>
                <div className='modalPopupHeader'>
                  <div className='modalTitle'>{ModalPopupTitle}</div>
                  {closeModalPopup && <div className='modalCloseIcon'><img src={CloseIcon} alt="close" onClick={closeModalPopup}/></div>}
                </div>
                <div className='modalPopupBody'>
                <div className="modalBodyContainer">
                        {children}
                </div>
                </div>
                {
                ShowModalPopupFooter
                  ? <div className='modalPopupFooter'>
                    <div className='footerButton'>
                      {
                      FooterActionPrimaryyBtnTxt
                        ? <PrimaryButton text={FooterActionPrimaryyBtnTxt} onClick={PrimaryActionBtnOnclick} disabled={disablePrimaryActionBtn}></PrimaryButton>
                        : null
                      }
                      {
                      FooterActionBtnTxt
                        ? <OutlineButton text={FooterActionBtnTxt} onClick={ActionBtnOnclick}></OutlineButton>
                        : null
                      }
                      {
                      FooterSecondaryBtnTxt
                        ? <OutlineButton text={FooterSecondaryBtnTxt} onClick={SecondryBtnOnclick}></OutlineButton>
                        : null
                      }
                      {
                      FooterPrimaryBtnTxt
                        ? <PrimaryButton text={FooterPrimaryBtnTxt} onClick={PrimaryBtnOnclick}></PrimaryButton>
                        : null
                      }
                    </div>
                </div>
                  : null}
            </div>
          </div>
        </div>
  )
}

export default ModalPopupInner;

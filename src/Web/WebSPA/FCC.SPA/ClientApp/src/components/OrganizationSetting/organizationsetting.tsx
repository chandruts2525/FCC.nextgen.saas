import { Breadcrumb, Card, Col, Label, Panel, PrimaryButton, Row, SearchBox, SecondaryButton, TextField, PanelType, Treeview } from "../CommonComponent";
import { alphaNumericOnlyWithSpace, alphaNumericOnlyWithSpaceOnPaste, checkValidationError } from "../../utils/Validators";
import AddCompany from "./AddCompany";
import AddDepartment from "./AddDepartment";
import AddGroup from "./AddGroup";
import AddRegion from "./AddRegion";
import AddYard from "./AddYard";
import OrganizationFlow from "./OrganizationFlow";
import "./organizationsetting.scss";

interface IProps {
  breadcrumlist: any,
  openPanel: any,
  dismissPanel: any,
  isOpen: any,
  openPanelRegion: any,
  isOpenRegion: any,
  dismissPanelRegion: any,
  openPanelYard: any;
  dismissPanelYard: any;
  openPanelDepartment: any;
  dismissPanelDepartment: any,
  isOpenYard: any,
  isOpenDepartment: any,
  openPanelGroup: any,
  dismissPanelGroup: any,
  isOpenGroup: any,
  openPanelPreview: any,
  dismissPanelPreview: any,
  isOpenPreview: any,
  treeData: any,
  customTreeUI: any,
  collectData: any,
  formData: any,
  onSubmit: any,
  setAction: any,
  action: any,
  setConfirmationPopup: any,
  isTextboxVisible: any,
}
const OrganizationSetting = ({ breadcrumlist, openPanel, dismissPanel, isOpen, openPanelRegion, isOpenRegion,
  dismissPanelRegion, openPanelYard, dismissPanelYard, openPanelDepartment, dismissPanelDepartment, isOpenYard,
  isOpenDepartment, openPanelGroup, dismissPanelGroup, isOpenGroup, openPanelPreview, dismissPanelPreview,
  isOpenPreview, treeData, customTreeUI, collectData, formData, onSubmit, setAction, action, setConfirmationPopup, isTextboxVisible }: IProps) => {
  return (<>
    <div className="breadcrumb-block">
      <Card>
        <Breadcrumb items={breadcrumlist} />
      </Card>
    </div>
    <div className="FCC_right-Content card-container">
      <Card>
        <div className="organization-hierarchy-container">
          <div className="top-section mb-3">
            <p className="heading-txt pull-left">Organization Settings</p>
            <div className="pull-right my-sm-0 my-3">
              <SecondaryButton text="Group" onClick={openPanelGroup} />
              <PrimaryButton text="Preview" onClick={openPanelPreview}></PrimaryButton>
            </div>
          </div>
          <Row>
            <Col xs={12} sm={12} md={4} lg={4} xl={3} className="mb-3">
              <Label required>Corporate/Organization Name</Label>
              {isTextboxVisible &&
                <TextField placeholder="Enter Corporate/Organization Name" onChange={(e: any) => collectData('businessEntityName', e?.target?.value)}
                  value={formData?.businessEntityName}
                  onKeyPress={(e: any) => alphaNumericOnlyWithSpace(e)}
                  onPaste={(e: any) => {
                    alphaNumericOnlyWithSpaceOnPaste(e);
                  }}
                  {...(checkValidationError('required', formData?.businessEntityName, undefined, "Corporate/Organization Name field cannot be empty"))}
                />
              }
            </Col>
            <Col xs={12} sm={12} md={2} lg={1} xl={1} className="mb-3 ps-0">
              {isTextboxVisible &&
                <PrimaryButton text="Add" className="corporate-add" onClick={onSubmit} disabled={!formData?.businessEntityName || formData?.businessEntityName?.length <= 0} />
              }
            </Col>
          </Row>

          {/* Add Company slide out */}
          <Panel
            isOpen={isOpen?.visible}
            onDismiss={() => dismissPanel(false)}
            headerText={'Add Company'}
          >
            <AddCompany dismissPanel={dismissPanel} />
          </Panel>
          {/* Add Region slide out */}
          <Panel
            isOpen={isOpenRegion?.visible}
            onDismiss={() => dismissPanelRegion(false)}
            headerText={'Add Region'}
            slideType="small"
          >
            <AddRegion dismissPanelRegion={dismissPanelRegion} />
          </Panel>
          {/* Add Yard slide out */}
          <Panel
            isOpen={isOpenYard?.visible}
            onDismiss={() => dismissPanelYard(false)}
            headerText={'Add Yard'}
          >
            <AddYard dismissPanelYard={dismissPanelYard} />
          </Panel>
          {/* Add Department slide out */}
          <Panel
            isOpen={isOpenDepartment?.visible}
            onDismiss={() => dismissPanelDepartment(false)}
            headerText={'Add Department'}
            slideType="small"
          >
            <AddDepartment dismissPanelDepartment={dismissPanelDepartment} />
          </Panel>
          {/* Add group Panel */}
          <Panel
            isOpen={isOpenGroup?.visible}
            onDismiss={() => dismissPanelGroup(false)}
            headerText={'Add Group'}
            slideType="extra-small"
          >
            <AddGroup dismissPanelGroup={dismissPanelGroup} />
          </Panel>
          {/* Organization Tree Panel */}
          <Panel
            isOpen={isOpenPreview?.visible}
            onDismiss={() => dismissPanelPreview(false)}
            headerText={'Organization Tree Preview'}
            slideType="small"
          >
            <div className="custom_kendo_tree">
              <Treeview
                treeData={treeData}
                checkboxes={false}
                customRendering={customTreeUI}
              />
            </div>
          </Panel>
          {/* Manage Organization Hierarchy  */}
          <OrganizationFlow
            openPanel={openPanel}
            openPanelRegion={openPanelRegion}
            openPanelYard={openPanelYard}
            openPanelDepartment={openPanelDepartment}
            setAction={setAction}
            formData={formData}
            onSubmit={onSubmit}
            setConfirmationPopup={setConfirmationPopup}
            isOptionsVisible={!isTextboxVisible}
          />
        </div>
      </Card>
    </div>
  </>
  );
};

export default OrganizationSetting;;
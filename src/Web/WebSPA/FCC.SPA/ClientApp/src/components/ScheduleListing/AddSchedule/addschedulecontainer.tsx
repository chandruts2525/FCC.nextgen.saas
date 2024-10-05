import React, { useState } from "react";
import AddSchedule from "./addschedule";

const AddScheduleContainer = ({ dismissPanel, formData, setFormData, onSave, onUpdate, isFormValid, unitComponentOptions }: any) => {

  const [isTooltipVisible, setTooltipVisible] = useState(false);

  const handleInfoClick = () => {
    setTooltipVisible(!isTooltipVisible);
  };

  const hideInfoTooltip = () => {
    setTooltipVisible(false);
  };

  const statusOptions: any = [
    { value: 1, label: 'Active' },
    { value: 0, label: 'Inactive' }
  ];

  return (
    <AddSchedule
      handleInfoClick={handleInfoClick}
      statusOptions={statusOptions}
      unitComponentOptions={unitComponentOptions}
      formData={formData}
      setFormData={setFormData}
      onSave={onSave}
      onUpdate={onUpdate}
      isFormValid={isFormValid}
      hideInfoTooltip={hideInfoTooltip}
      dismissPanel={dismissPanel}
    />
  );
};

export default AddScheduleContainer;

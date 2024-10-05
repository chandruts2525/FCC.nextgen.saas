import SecondaryButton from "../Button/SecondaryButton"
import { exportIcon } from '../../../assets/images';
import { useEffect, useRef } from "react";

const Export = ({excelExportClick,showExportOption,excelExport,setShowExportOption,exportbtnName,exportDropdownClassName}:any) => {
    
    const wrapperRef = useRef<any>(null);

    useEffect(() => {
        document.addEventListener("click", handleClickOutside, false);
        return () => {
        document.removeEventListener("click", handleClickOutside, false);
        };
    }, []);

    const handleClickOutside = (event: any) => {
        if (event.target.closest('.export-block')) {
        setShowExportOption(true)
        }
        else {
        setShowExportOption(false)
        }
    }
    return(<>
        <SecondaryButton text="Export" btnRef={wrapperRef} className={`export-block ${exportbtnName}`} onClick={excelExportClick}>
            <img src={exportIcon} alt="Export" />
        </SecondaryButton>
        {showExportOption ? (
            <div className={`export-dropdown ${exportDropdownClassName}`}>
            <ul>
                <li onClick={() => excelExport(true, 'excel')} title="XLSX">
                XLSX
                </li>
                <li onClick={() => excelExport(false, 'pdf')} title="PDF">
                PDF
                </li>
                <li onClick={() => excelExport(true, 'csv')} title="CSV">
                CSV
                </li>
            </ul>
            </div>
        ) : (
            <></>
        )}
      </>
    )
}

export default Export
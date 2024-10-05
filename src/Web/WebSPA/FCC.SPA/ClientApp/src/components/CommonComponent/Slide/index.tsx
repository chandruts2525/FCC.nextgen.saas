import React, { ReactNode } from 'react';
import ReactDOM from 'react-dom';
import './slide.scss';
import { motion, AnimatePresence } from "framer-motion"
import Theme from "../../../utils/theme"
interface SlideProps {
    isOpen: boolean;
    onDismiss: () => void;
    children: ReactNode;
    headerText: any,
    slideType?:any
}

const SlideComp: React.FC<SlideProps> = ({ isOpen, onDismiss, children, headerText, slideType }) => {
    const modalVariants = {
        hidden: { opacity: 0, x: 200 },
        visible: { opacity: 1, x: 0 },
    };
    const Slide = (
        <div className="Slide-overlay" style={Theme as React.CSSProperties}>
            <AnimatePresence>
                {isOpen && (
                    <motion.div className={`Slide ${slideType}`}
                        initial='hidden'
                        animate={isOpen ? 'visible' : 'hidden'}
                        transition={{ duration: 0.2 }}
                        variants={modalVariants}
                        exit={{ opacity: 0, y: -500 }}
                    >
                        <div className="Slide-Header">
                            <span className="Slide-Title">{headerText}</span>
                            <button className="Slide-close" onClick={onDismiss}>
                                &times;
                            </button>
                        </div>
                        <div className="Slide-content">{children}</div>
                    </motion.div>
                )}
            </AnimatePresence>
         </div>
    );

    return isOpen ? ReactDOM.createPortal(Slide, document.body) : null;
};

export default SlideComp;
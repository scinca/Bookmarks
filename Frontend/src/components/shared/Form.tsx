import {
    Button,
    type ButtonProps,
    FieldError as RACFieldError,
    type FieldErrorProps,
    Form as RACForm,
    type FormProps,
    Label as RACLabel,
    type LabelProps,
    Text,
    type TextProps
} from "react-aria-components";
import styles from './Form.module.css';

export function Form(props: FormProps) {
    return <RACForm {...props} />;
}

export function Label(props: LabelProps) {
    return <RACLabel {...props} />;
}

export function FieldError(props: FieldErrorProps) {
    return <RACFieldError {...props} />;
}

export function Description(props: TextProps) {
    return <Text slot="description" className={styles.fieldButton} {...props} />;
}

export function FieldButton(props: ButtonProps) {
    return <Button {...props} className={styles.fieldDescription}/>;
}


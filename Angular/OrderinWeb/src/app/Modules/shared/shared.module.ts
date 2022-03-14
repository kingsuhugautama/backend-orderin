import { DialogModule, TooltipModule } from '@syncfusion/ej2-angular-popups';
import { ToolbarModule, TabModule, SidebarModule, ContextMenuModule, TreeViewModule } from '@syncfusion/ej2-angular-navigations';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DatePickerModule, DateTimePickerModule } from '@syncfusion/ej2-angular-calendars';
import { ComboBoxModule, DropDownListModule, AutoCompleteModule } from '@syncfusion/ej2-angular-dropdowns';
import { GridAllModule } from '@syncfusion/ej2-angular-grids';
import { NumericTextBoxModule } from '@syncfusion/ej2-angular-inputs';
import { RichTextEditorModule } from '@syncfusion/ej2-angular-richtexteditor';
import { TreeGridAllModule } from '@syncfusion/ej2-angular-treegrid';
import { AccumulationChartModule, ChartModule } from '@syncfusion/ej2-angular-charts';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RequestHttpInterceptor } from 'src/app/Utility/Interceptor/RequestHttpInterceptor';

@NgModule({
  declarations: [
    // PisonLookupDefaultComponent,
    // PisonLookupDefaultComponent,
    // PisonGridDefaultComponent,
    // PisonLookupCheckboxComponent,
    // ModifyFormDirective,
    // CustomCurrencyPipe,
    // AlphabetFormDirective
  ],
  exports: [
    // PisonLookupDefaultComponent,
    // PisonLookupDefaultComponent,
    // PisonGridDefaultComponent,
    // PisonLookupCheckboxComponent,
    // ModifyFormDirective,
    // CustomCurrencyPipe,
    // AlphabetFormDirective,

    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,

    GridAllModule,
    NumericTextBoxModule,
    ToolbarModule,
    TreeGridAllModule,
    ComboBoxModule,
    DropDownListModule,
    TabModule,
    NgxSpinnerModule,
    RichTextEditorModule,
    DialogModule,
    DatePickerModule,
    DateTimePickerModule,
    ContextMenuModule,
    AutoCompleteModule,
    TreeViewModule,
    SidebarModule,
    ChartModule,
    AccumulationChartModule,
    TooltipModule
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,

    GridAllModule,
    NumericTextBoxModule,
    TreeGridAllModule,
    TreeViewModule,
    ComboBoxModule,
    DropDownListModule,
    ToolbarModule,
    TabModule,
    NgxSpinnerModule,
    RichTextEditorModule,
    DialogModule,
    DatePickerModule,
    DateTimePickerModule,
    ContextMenuModule,
    SidebarModule,
    ChartModule,
    AccumulationChartModule,
    TooltipModule
  ],
  providers:[{
    provide:HTTP_INTERCEPTORS,
    useClass:RequestHttpInterceptor,
    multi:true
  }],
  schemas:[CUSTOM_ELEMENTS_SCHEMA]
})
export class SharedModule { }
